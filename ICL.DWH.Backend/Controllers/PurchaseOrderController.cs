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
using ICL.DWH.Backend.Core;

namespace ICL.DWH.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly IPurchaseOrderService _purchaseOrderService;
        private readonly IProductService _productService;
        private readonly IProductDetailService _productDetailService;
        private readonly DataContext _dataContext;
        private static readonly HttpClient _httpClient = new HttpClient();

        public PurchaseOrderController(IPurchaseOrderService purchaseOrderService, IProductService productService, IProductDetailService productDetailService, DataContext dataContext)
        {
            _purchaseOrderService = purchaseOrderService;
            _productService = productService;
            _productDetailService = productDetailService;
            _dataContext = dataContext;
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
                            product.ProductGS1Code = productDetail.trade_item_product_gs1;
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
                            po.DeliveryStatus = PurchaseOrderStatus.Failed;
                            po.SubmitStatus = "Submitted";
                            _purchaseOrderService.UpdatePurchaseOrder(po);
                        }
                        else
                        {
                            var transactionId = scmResponse.Transaction.TransactionId;

                            po.ErrorMessage = "";
                            po.DeliveryStatus = PurchaseOrderStatus.Delivered;
                            po.SCMID = new Guid(transactionId);
                            po.SubmitStatus = "Submitted";
                            _purchaseOrderService.UpdatePurchaseOrder(po);
                        }
                    }
                }

                //-------------------------------------
                ServiceBusClient client = new ServiceBusClient("Endpoint=sb://ghsc-icl.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=T6Rv/GTQAb2p+UYm/yJL92EIvfQ4OcfRy3kY9xV+5/E=");
                ServiceBusSender sender = client.CreateSender("asn");

                using (ServiceBusMessageBatch message = await sender.CreateMessageBatchAsync())
                {
                    message.TryAddMessage(new ServiceBusMessage(po.AsnFile.ToString()));
                    try
                    {
                        await sender.SendMessagesAsync(message);
                    }
                    finally
                    {
                        await sender.DisposeAsync();
                        await client.DisposeAsync();
                    }
                }
                //-------------------------------------

                return Ok(new { message = "Successful" });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("middlewarepost/{bookingNo}")]
        public async Task<IActionResult> PostOrderToMiddleWare(string bookingNo)
        {
            try
            {
                PurchaseOrder po = _purchaseOrderService.GetPurchaseOrders().Where(x => x.BookingNo == bookingNo).FirstOrDefault();
                var products = _productService.GetProductsByPOUUID(po.uuid);
                po.products = products.ToList();
                po.SubmitStatus = "Submitted";
                _purchaseOrderService.UpdatePurchaseOrder(po);

                ServiceBusClient client = new ServiceBusClient("Endpoint=sb://ghsc-icl.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=T6Rv/GTQAb2p+UYm/yJL92EIvfQ4OcfRy3kY9xV+5/E=");
                ServiceBusSender sender = client.CreateSender("asn");

                using (ServiceBusMessageBatch message = await sender.CreateMessageBatchAsync())
                {
                    message.TryAddMessage(new ServiceBusMessage(po.AsnFile.ToString()));
                    try
                    {
                        await sender.SendMessagesAsync(message);
                    }
                    finally
                    {
                        await sender.DisposeAsync();
                        await client.DisposeAsync();
                    }
                }

                return Ok(new { message = "Successfully sent ASN" });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("statistics")]
        public IActionResult GetMiddlewareStatistics()
        {
            try
            {
                return Ok(_dataContext.Statistics.ToList());
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }

    public class ErrorMessage
    {
        public string Message { get; set; }
    }
}
