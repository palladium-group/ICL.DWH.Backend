using ICL.DWH.Backend.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICL.DWH.Backend.Core
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
        }

        public virtual void EnsureSeeded()
        {
        }

        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
    }
}
