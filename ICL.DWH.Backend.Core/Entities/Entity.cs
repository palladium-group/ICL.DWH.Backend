using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICL.DWH.Backend.Core.Entities
{
    public class Entity
    {
        [Key]
        public virtual Guid Id { get; set; }
        public DateTime CreateDate { get; set; }

        protected Entity() 
        {
            Id = Guid.NewGuid(); 
        }
    }
}
