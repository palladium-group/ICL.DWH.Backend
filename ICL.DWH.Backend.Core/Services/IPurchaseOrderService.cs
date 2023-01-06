using ICL.DWH.Backend.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICL.DWH.Backend.Core.Services
{
    public interface IPurchaseOrderService
    {
        PurchaseOrder NewPurchaseOrder(PurchaseOrder purchaseOrder);
        IEnumerable<PurchaseOrder> GetPurchaseOrders();
        void UpdatePurchaseOrderByScmId(string bookingId, Guid scmId);
        void UpdatePurchaseOrderAsFailed(string bookingId);
    }
}
