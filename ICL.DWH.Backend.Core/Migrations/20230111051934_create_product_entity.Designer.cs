// <auto-generated />
using System;
using ICL.DWH.Backend.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ICL.DWH.Backend.Core.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230111051934_create_product_entity")]
    partial class create_product_entity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ICL.DWH.Backend.Core.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("LineItemId")
                        .HasColumnType("text");

                    b.Property<string>("OrderDetails")
                        .HasColumnType("text");

                    b.Property<Guid>("PoUuid")
                        .HasColumnType("uuid");

                    b.Property<string>("ProductCode")
                        .HasColumnType("text");

                    b.Property<Guid?>("PurchaseOrderId")
                        .HasColumnType("uuid");

                    b.Property<string>("Quantity")
                        .HasColumnType("text");

                    b.Property<string>("SKULineNo")
                        .HasColumnType("text");

                    b.Property<string>("UnitDimension")
                        .HasColumnType("text");

                    b.Property<string>("UnitRate")
                        .HasColumnType("text");

                    b.Property<string>("UnitVolume")
                        .HasColumnType("text");

                    b.Property<string>("UnitWeight")
                        .HasColumnType("text");

                    b.Property<Guid>("uuid")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PurchaseOrderId");

                    b.ToTable("ft_products");
                });

            modelBuilder.Entity("ICL.DWH.Backend.Core.Entities.PurchaseOrder", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AsnFile")
                        .HasColumnType("text");

                    b.Property<DateTime>("BookingDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("BookingNo")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ErrorMessage")
                        .HasColumnType("text");

                    b.Property<string>("ProcessType")
                        .HasColumnType("text");

                    b.Property<Guid?>("SCMID")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<Guid>("uuid")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("ft_purchase_orders");
                });

            modelBuilder.Entity("ICL.DWH.Backend.Core.Entities.Product", b =>
                {
                    b.HasOne("ICL.DWH.Backend.Core.Entities.PurchaseOrder", null)
                        .WithMany("products")
                        .HasForeignKey("PurchaseOrderId");
                });

            modelBuilder.Entity("ICL.DWH.Backend.Core.Entities.PurchaseOrder", b =>
                {
                    b.Navigation("products");
                });
#pragma warning restore 612, 618
        }
    }
}
