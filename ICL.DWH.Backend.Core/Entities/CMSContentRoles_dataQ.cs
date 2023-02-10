using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICL.DWH.Backend.Core.Entities
{
  
    public class CMSContentRoles_dataQ : Entity
    {
        public string? RoleName{ get; set; }
        public Guid? CMS_content_roles_id { get; set; }
        public bool? Status { get; set; }

    }
}
