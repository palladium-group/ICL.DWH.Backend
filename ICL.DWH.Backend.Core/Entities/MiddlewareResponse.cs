using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICL.DWH.Backend.Core.Entities
{
    public class MiddlewareResponse
    {
        public string? BookingNo { get; set; }
        public string? ErrorString { get; set; }
        public string? DeliveryStatus { get; set; }
        public Guid? SCMID { get; set; }
    }
}
