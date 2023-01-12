using ICL.DWH.Backend.Core.Entities;
using ICL.DWH.Backend.Core.Repository;
using Newtonsoft.Json;
using System.Xml.Serialization;
using ICL.DWH.Backend.Core.Utils;

namespace ICL.DWH.Backend.Core.Services
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        private static readonly HttpClient _httpClient = new HttpClient();

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

        public PurchaseOrder UpdatePurchaseOrder(PurchaseOrder purchaseOrder)
        {
            try
            {
                return _purchaseOrderRepository.Update(purchaseOrder);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public PurchaseOrder FindByBoookingNo(string BookingNo)
        {
            try
            {
                var result = _purchaseOrderRepository.GetAll(x => x.BookingNo == BookingNo).FirstOrDefault();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdatePurchaseOrderAsFailed(string bookingId, string errorMessage)
        {
            try
            {
                var purchaseOrder = _purchaseOrderRepository.GetAll(x => x.BookingNo == bookingId && x.Status == PurchaseOrderStatus.Pending).FirstOrDefault();
                if (purchaseOrder != null)
                {
                    purchaseOrder.Status = PurchaseOrderStatus.Failed;
                    purchaseOrder.ErrorMessage = errorMessage;
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

        public string ValidatePurchaseOrder(PurchaseOrder purchaseOrder)
        {
            try
            {
                if (purchaseOrder != null)
                {
                    if (purchaseOrder.BookingDate != null && purchaseOrder.BookingNo != null)
                    {
                        return "valid";
                    }
                    else
                    {
                        return "not valid";
                    }
                }
                else
                {
                    return "not valid";
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
