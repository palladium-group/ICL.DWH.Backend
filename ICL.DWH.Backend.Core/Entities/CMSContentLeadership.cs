using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICL.DWH.Backend.Core.Entities
{
    [Table("cms_content_leadership")]
    public class CMSContentLeadership : Entity
    {
        public string? Name { get; set; }
        public string? Position { get; set; }
        public string? Image { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Bio { get; set; }
        public string? Alignment { get; set; }
    }
}
