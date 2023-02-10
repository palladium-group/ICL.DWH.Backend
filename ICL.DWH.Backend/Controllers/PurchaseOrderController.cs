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
using Microsoft.Extensions.Logging;

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
        private readonly IConfiguration _configuration;
        private static readonly HttpClient _httpClient = new HttpClient();
        private readonly ILogger<PurchaseOrderController> _logger;

        public PurchaseOrderController(
            IPurchaseOrderService purchaseOrderService,
            IConfiguration configuration,
            IProductService productService, 
            IProductDetailService productDetailService, 
            ILogger<PurchaseOrderController> logger,
            DataContext dataContext)
        {
            _purchaseOrderService = purchaseOrderService;
            _productService = productService;
            _productDetailService = productDetailService;
            _dataContext = dataContext;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost("inbound")]
        public IActionResult SaveInboundPO(PurchaseOrder purchaseOrder)
        {
            try
            {
                purchaseOrder.ProcessType = "Inbound";
                return Ok(_purchaseOrderService.NewPurchaseOrder(purchaseOrder));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet("inbound/{validated}")]
        public IActionResult GetInboundPurchaseOrders(int validated)
        {
            try
            {
                if ((PurchaseOrderStatus)validated == PurchaseOrderStatus.Delivered)
                {
                    return Ok(GetPurchaseOrders().Where(x => x.ProcessType == "Inbound" && x.DeliveryStatus == PurchaseOrderStatus.Delivered).OrderByDescending(y => y.CreateDate));
                }
                return Ok(GetPurchaseOrders().Where(x=>x.ProcessType=="Inbound" && x.DeliveryStatus != PurchaseOrderStatus.Delivered).OrderByDescending(y => y.CreateDate));
            }
            catch (Exception e)
            {
                throw;
            }
        }

        [HttpPost("outbound")]
        public IActionResult SaveOutboundPO(PurchaseOrder purchaseOrder)
        {
            try
            {
                purchaseOrder.ProcessType = "Outbound";
                return Ok(_purchaseOrderService.NewPurchaseOrder(purchaseOrder));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet("outbound/{validated}")]
        public IActionResult GetOutboundPurchaseOrders(int validated)
        {
            try
            {
                if ((PurchaseOrderStatus)validated == PurchaseOrderStatus.Delivered)
                {
                    return Ok(GetPurchaseOrders().Where(x => x.ProcessType == "Outbound" && x.DeliveryStatus == PurchaseOrderStatus.Delivered).OrderByDescending(y => y.CreateDate));
                }
                return Ok(GetPurchaseOrders().Where(x => x.ProcessType == "Outbound" && x.DeliveryStatus != PurchaseOrderStatus.Delivered).OrderByDescending(y => y.CreateDate));
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private List<PurchaseOrder> GetPurchaseOrders()
        {
            List<PurchaseOrder> purchaseOrders = _purchaseOrderService.GetPurchaseOrders().ToList();

            foreach (PurchaseOrder po in purchaseOrders)
            {
                var products = _productService.GetProductsByPOUUID(po.uuid);

                foreach (Product product in products)
                {
                    try
                    {
                        ProductDetail productDetail = _productDetailService.GetProductDetailByProductCode(product.ProductCode);
                        if (productDetail != null)
                        {
                            product.TradeItemName = productDetail.trade_item_name;
                            product.TradeItemCategory = productDetail.trade_item_category;
                            product.TradeItemProduct = productDetail.trade_item_product;
                            product.ProgramArea = productDetail.program_area;
                            product.ProductGS1Code = productDetail.trade_item_product_gs1;
                        }
                    }
                    catch { }
                }
                po.products = products.ToList();
            }

            return purchaseOrders;
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

        [HttpPost("UpdatePurchaseOrderStatus")]
        public IActionResult UpdatePurchaseOrderAsFailed(MiddlewareResponse response)
        {
            try
            {
                PurchaseOrder po = _purchaseOrderService.GetPurchaseOrders()
                    .Where(x => x.BookingNo == response.BookingNo).FirstOrDefault();

                po.ErrorMessage = response.ErrorString;
                po.DeliveryStatus = response.DeliveryStatus == "Failed" ? PurchaseOrderStatus.Failed : PurchaseOrderStatus.Delivered;
                po.SCMID = response.SCMID;
                _purchaseOrderService.UpdatePurchaseOrder(po);
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
        public async Task<IActionResult> PostOrderToMiddleWare(string bookingNo)
        {
            try
            {
                PurchaseOrder po = _purchaseOrderService.GetPurchaseOrders().Where(x => x.BookingNo == bookingNo).FirstOrDefault();
                var products = _productService.GetProductsByPOUUID(po.uuid);
                po.products = products.ToList();
                po.SubmitStatus = "Submitted";
                _purchaseOrderService.UpdatePurchaseOrder(po);
                var connectionString = _configuration.GetConnectionString("ServiceBus");
                _logger.LogInformation(connectionString);
                ServiceBusClient client = new ServiceBusClient(connectionString);
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

        [HttpGet("statistics/{processtype}")]
        public IActionResult GetMiddlewareStatistics(string processtype)
        {
            try
            {
                return Ok(_dataContext.Statistics.Where(x => x.processtype.ToLower() == processtype.ToLower()));
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
