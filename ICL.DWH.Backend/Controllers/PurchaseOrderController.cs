using ICL.DWH.Backend.Core.Entities;
using ICL.DWH.Backend.Core.Repository;
using ICL.DWH.Backend.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ICL.DWH.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly IPurchaseOrderService _purchaseOrderService;

        public PurchaseOrderController(IPurchaseOrderService purchaseOrderService)
        {
            _purchaseOrderService = purchaseOrderService;
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
                return Ok(_purchaseOrderService.GetPurchaseOrders());
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
        public IActionResult UpdatePurchaseOrderAsFailed(string bookingNo)
        {
            try
            {
                _purchaseOrderService.UpdatePurchaseOrderAsFailed(bookingNo);
                return Ok(new { message = "Updated successfully" });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
