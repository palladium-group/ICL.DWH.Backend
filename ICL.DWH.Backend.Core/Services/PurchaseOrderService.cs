using ICL.DWH.Backend.Core.Entities;
using ICL.DWH.Backend.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICL.DWH.Backend.Core.Services
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;

        public PurchaseOrderService(IPurchaseOrderRepository purchaseOrderRepository)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
        }

        public IEnumerable<PurchaseOrder> GetPurchaseOrders()
        {
            return _purchaseOrderRepository.GetAll();
        }

        public PurchaseOrder NewPurchaseOrder(PurchaseOrder purchaseOrder)
        {
            try
            {
                var result = _purchaseOrderRepository.GetAll(x => x.BookingNo == purchaseOrder.BookingNo).FirstOrDefault();
                if (result == null)
                {
                    return _purchaseOrderRepository.Create(purchaseOrder);
                }
                result.AsnFile = purchaseOrder.AsnFile;
                return _purchaseOrderRepository.Update(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdatePurchaseOrderAsFailed(string bookingId)
        {
            try
            {
                var purchaseOrder = _purchaseOrderRepository.GetAll(x => x.BookingNo == bookingId && x.Status == PurchaseOrderStatus.Pending).FirstOrDefault();
                if (purchaseOrder != null)
                {
                    purchaseOrder.Status = PurchaseOrderStatus.Failed;
                    _purchaseOrderRepository.Update(purchaseOrder);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void UpdatePurchaseOrderByScmId(string bookingId, Guid scmId)
        {
            try
            {
                var purchaseOrder = _purchaseOrderRepository.GetAll(x => x.BookingNo == bookingId).FirstOrDefault();
                if (purchaseOrder != null)
                {
                    purchaseOrder.SCMID = scmId;
                    purchaseOrder.Status = PurchaseOrderStatus.Delivered;
                    _purchaseOrderRepository.Update(purchaseOrder);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
