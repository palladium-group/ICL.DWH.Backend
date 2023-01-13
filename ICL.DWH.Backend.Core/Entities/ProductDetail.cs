using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICL.DWH.Backend.Core.Entities
{
    [Table("dim_products")]
    public class ProductDetail : Entity
    {
        public string? trade_item_uid { get; set; }
        public string? trade_item_code { get; set; }
        public string? trade_item_name { get; set; }
        public string? trade_item_category { get; set; }
        public string? trade_item_product { get; set; }
        public string? program_area { get; set; }
        public string? trade_item_product_gs1 { get; set; }
    }
}
