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

            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = new System.Guid("26bff42a-c785-47e9-808f-f3a402ac6ee8"),
                    RoleName = "HQ.User",
                    CreateDate = DateTime.Now,
                },
                new Role
                {
                    Id = new System.Guid("64dbfc18-5508-4af5-aecd-e98cd731fd15"),
                    RoleName = "Country.User",
                    CreateDate = DateTime.Now,
                },
                new Role
                {
                    Id = new System.Guid("44b8bcf1-f444-4eb0-958e-64c77f24e5c7"),
                    RoleName = "Washington.User",
                    CreateDate = DateTime.Now,
                }
            );
        }

        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDetail> productDetails { get; set; }
        public DbSet<Statistic> Statistics { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
