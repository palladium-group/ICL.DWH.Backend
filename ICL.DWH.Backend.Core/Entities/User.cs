using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICL.DWH.Backend.Core.Entities
{
    [Table("icl_users")]
    public class User : Entity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
