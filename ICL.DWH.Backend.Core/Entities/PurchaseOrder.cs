using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICL.DWH.Backend.Core.Entities
{
    public class PurchaseOrder : Entity
    {
        public string? BookingNo { get; set; }
        public DateTime BookingDate { get; set; }
        public Guid? SCMID { get; set; }
        public string? AsnFile { get; set; }
    }
}
