using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICL.DWH.Backend.Core.Entities
{
    [Table("ft_purchase_orders")]
    public class PurchaseOrder : Entity
    {
        public Guid uuid { get; set; }
        public string? BookingNo { get; set; }
        public DateTime BookingDate { get; set; }
        public Guid? SCMID { get; set; }
        public string? AsnFile { get; set; }
        public PurchaseOrderStatus DeliveryStatus { get; set; }
        public string? ErrorMessage { get; set; }
        public string? ProcessType { get; set; }
        public string? PlaceOfReceipt { get; set; }
        public string? PlaceOfDelivery { get; set; }
        public string? SubmitStatus { get; set; }
        public string? TransportationMode { get; set; }
        public Guid? shipmentid { get; set; }
        public List<Product>? products { get; set; }
    }

    public enum PurchaseOrderStatus
    {
        Pending,
        Delivered,
        Failed
    }
}
