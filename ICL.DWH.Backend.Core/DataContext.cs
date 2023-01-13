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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
               .Entity<Statistic>()
               .ToView("statistics")
               .HasKey(t => t.createdate);
        }

        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDetail> productDetails { get; set; }
        public DbSet<Statistic> Statistics { get; set; } 
    }
}
