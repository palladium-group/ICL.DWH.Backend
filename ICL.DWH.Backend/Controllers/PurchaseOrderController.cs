using Azure.Messaging.ServiceBus;
using ICL.DWH.Backend.Core.Entities;
using ICL.DWH.Backend.Core.Repository;
using ICL.DWH.Backend.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ICL.DWH.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly IPurchaseOrderService _purchaseOrderService;
        private readonly IProductService _productService;

        public PurchaseOrderController(IPurchaseOrderService purchaseOrderService, IProductService productService)
        {
            _purchaseOrderService = purchaseOrderService;
            _productService = productService;
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
                throw;
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

        [HttpGet("{bookingNo}")]
        public IActionResult ValidatePO(string bookingNo)
        {
            try
            {
                var response = _purchaseOrderService.ValidatePurchaseOrder(bookingNo);
                return Ok(new { message = response });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{bookingNo}")]
        public async Task<IActionResult> PostOrderToSCM(string bookingNo)
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.QueryString.Add("bookingNo", bookingNo);
                string result = webClient.DownloadString("https://localhost:7014/api/PurchaseOrder");

                byte[] byteArray = Encoding.ASCII.GetBytes(result);
                MemoryStream stream = new MemoryStream(byteArray);
                XmlSerializer serial = new XmlSerializer(typeof(PurchaseOrder));
                PurchaseOrder po = (PurchaseOrder)serial.Deserialize(stream);

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(po.AsnFile.ToString());

                ServiceBusClient client = new ServiceBusClient("Endpoint=sb://ghsc-icl.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=T6Rv/GTQAb2p+UYm/yJL92EIvfQ4OcfRy3kY9xV+5/E=");
                ServiceBusSender sender = client.CreateSender("asn");

                using (ServiceBusMessageBatch message = await sender.CreateMessageBatchAsync())
                {
                    message.TryAddMessage(new ServiceBusMessage(xmlDoc.ToString()));
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
    }

    public class ErrorMessage
    {
        public string Message { get; set; }
    }
}
