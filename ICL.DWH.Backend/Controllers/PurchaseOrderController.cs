using Azure.Messaging.ServiceBus;
using ICL.DWH.Backend.Core.Entities;
using ICL.DWH.Backend.Core.Repository;
using ICL.DWH.Backend.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using ICL.DWH.Backend.Core.Utils;


namespace ICL.DWH.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly IPurchaseOrderService _purchaseOrderService;
        private readonly IProductService _productService;
        private readonly IProductDetailService _productDetailService;
        private static readonly HttpClient _httpClient = new HttpClient();

        public PurchaseOrderController(IPurchaseOrderService purchaseOrderService, IProductService productService, IProductDetailService productDetailService)
        {
            _purchaseOrderService = purchaseOrderService;
            _productService = productService;
            _productDetailService = productDetailService;
        }

        [HttpPost]
        public IActionResult NewPurchaseOrder(PurchaseOrder purchaseOrder)
        {
            try
            {
                return Ok(_purchaseOrderService.NewPurchaseOrder(purchaseOrder));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        public IActionResult GetPurchaseOrders()
        {
            try
            {
                List<PurchaseOrder> purchaseOrders = _purchaseOrderService.GetPurchaseOrders().ToList();

                foreach(PurchaseOrder po in purchaseOrders)
                {
                    var products = _productService.GetProductsByPOUUID(po.uuid);

                    foreach (Product product in products)
                    {
                        try
                        {
                            ProductDetail productDetail = _productDetailService.GetProductDetailByProductCode(product.ProductCode);
                            product.TradeItemName = productDetail.trade_item_name;
                            product.TradeItemCategory = productDetail.trade_item_category;
                            product.TradeItemProduct = productDetail.trade_item_product;
                            product.ProgramArea = productDetail.program_area;
                        }
                        catch { }
                    }

                    po.products = products.ToList();
                }

                return Ok(purchaseOrders);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        [HttpGet("{bookingNo}/{scmId}")]
        public IActionResult UpdatePurchaseOrderByScmId(string bookingNo, Guid scmId)
        {
            try
            {
                _purchaseOrderService.UpdatePurchaseOrderByScmId(bookingNo, scmId);
                return Ok(new { message = "Updated successfully" });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("UpdatePurchaseOrderAsFailed/{bookingNo}")]
        public IActionResult UpdatePurchaseOrderAsFailed(string bookingNo, [FromBody] ErrorMessage errorMessage)
        {
            try
            {
                _purchaseOrderService.UpdatePurchaseOrderAsFailed(bookingNo, errorMessage.Message);
                return Ok(new { message = "Updated successfully" });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("validate/{bookingNo}")]
        public IActionResult ValidatePO(string bookingNo)
        {
            try
            {
                PurchaseOrder po = _purchaseOrderService.GetPurchaseOrders().ToList().FirstOrDefault();
                var products = _productService.GetProductsByPOUUID(po.uuid);
                po.products = products.ToList();

                var response = _purchaseOrderService.ValidatePurchaseOrder(po);
                return Ok(new { message = response });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("post/{bookingNo}")]
        public async Task<IActionResult> PostOrderToSCM(string bookingNo)
        {
            try
            {
                PurchaseOrder po = _purchaseOrderService.GetPurchaseOrders().Where(x=>x.BookingNo==bookingNo).FirstOrDefault();
                var products = _productService.GetProductsByPOUUID(po.uuid);
                po.products = products.ToList();

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(po.AsnFile.ToString());
                //_purchaseOrderService.PostToSCMAsync(po.AsnFile, po.BookingNo);

                var requestContent = new StringContent("grant_type=password&username=fitexpress&password=FitExpress@2021");
                var response = await _httpClient.PostAsync("http://fitnewuat.hht.freightintime.com/token", requestContent);
                var responseBody = await response.Content.ReadAsAsync<SCMResponse>();
                var token = responseBody.access_token;
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var bookingRequestContent = new StringContent(po.AsnFile);
                var bookingResponse = await _httpClient.PostAsync("http://fitnewuat.hht.freightintime.com/api/v1/Booking/ProcessBooking", bookingRequestContent);
                var responseContent = await bookingResponse.Content.ReadAsStringAsync();

                if (bookingResponse.IsSuccessStatusCode)
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ArrayOfTransaction));
                    using (StringReader reader = new StringReader(responseContent))
                    {
                        var scmResponse = (ArrayOfTransaction)serializer.Deserialize(reader);

                        if (scmResponse.Transaction.Status == "Fail")
                        {
                            var errorString = JsonConvert.SerializeObject(responseContent);

                            po.ErrorMessage = errorString;
                            po.Status = PurchaseOrderStatus.Failed;
                            _purchaseOrderService.UpdatePurchaseOrder(po);
                        }
                        else
                        {
                            var transactionId = scmResponse.Transaction.TransactionId;

                            po.ErrorMessage = "";
                            po.Status = PurchaseOrderStatus.Delivered;
                            po.SCMID = new Guid(transactionId);
                            _purchaseOrderService.UpdatePurchaseOrder(po);
                        }
                    }
                }

                return Ok(new { message = "Successful" });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }

    public class ErrorMessage
    {
        public string Message { get; set; }
    }
}
