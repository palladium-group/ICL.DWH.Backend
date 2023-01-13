using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ICL.DWH.Backend.Core.Entities
{
    [Table("ft_products")]
    public class Product : Entity
    {
        public Guid uuid { get; set; }
        public Guid PoUuid { get; set; }
        public string? LineItemId { get; set; }
        public string? ProductCode { get; set; }
        public string? Quantity { get; set; }
        public string? UnitDimension { get; set; }
        public string? UnitVolume { get; set; }
        public string? UnitWeight { get; set; }
        public string? UnitRate { get; set; }
        public string? OrderDetails { get; set; }
        public string? SKULineNo { get; set; }
        public string? TradeItemName { get; set; }
        public string? TradeItemCategory { get; set; }
        public string? TradeItemProduct { get; set; }
        public string? ProgramArea { get; set; }
        public string? ProductGS1Code { get; set; }
        public string? SubmitStatus { get; set; }
    }
}
