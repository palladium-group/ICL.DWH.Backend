using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICL.DWH.Backend.Core.Entities
{
    public class Statistic
    {
        public DateTime createdate { get; set; }
        public int pending { get; set; }
        public int delivered { get; set; }
        public int failed { get; set; }
    }
}
