using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICL.DWH.Backend.Core.Entities
{
    [Table("cms_content_roles")]
    public class CMSContentRoles : Entity
    {
        public Guid? Id_roles { get; set; }
        public Guid? Id_content { get; set; }
        public int? Type { get; set; }
        public bool? Status { get; set; }
    }

}
