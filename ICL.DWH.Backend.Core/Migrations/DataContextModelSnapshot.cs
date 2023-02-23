﻿// <auto-generated />
using System;
using ICL.DWH.Backend.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ICL.DWH.Backend.Core.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ICL.DWH.Backend.Core.Entities.CMSContentImpact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Image")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("cms_content_impact");
                });

            modelBuilder.Entity("ICL.DWH.Backend.Core.Entities.CMSContentLeadership", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Alignment")
                        .HasColumnType("text");

                    b.Property<string>("Bio")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Image")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.Property<string>("Position")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("cms_content_leadership");
                });

            modelBuilder.Entity("ICL.DWH.Backend.Core.Entities.CMSContentRoles", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("Id_content")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("Id_roles")
                        .HasColumnType("uuid");

                    b.Property<bool?>("Status")
                        .HasColumnType("boolean");

                    b.Property<int?>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("cms_content_roles");
                });

            modelBuilder.Entity("ICL.DWH.Backend.Core.Entities.CMSContentRoles_dataQ", b =>
                {
                    b.Property<Guid?>("CMS_content_roles_id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("RoleName")
                        .HasColumnType("text");

                    b.Property<bool?>("Status")
                        .HasColumnType("boolean");

                    b.ToTable("CMSContentRoles_dataQ");
                });

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

                    b.Property<string>("ProductGS1Code")
                        .HasColumnType("text");

                    b.Property<string>("ProgramArea")
                        .HasColumnType("text");

                    b.Property<Guid?>("PurchaseOrderId")
                        .HasColumnType("uuid");

                    b.Property<string>("Quantity")
                        .HasColumnType("text");

                    b.Property<string>("SKULineNo")
                        .HasColumnType("text");

                    b.Property<string>("SubmitStatus")
                        .HasColumnType("text");

                    b.Property<string>("TradeItemCategory")
                        .HasColumnType("text");

                    b.Property<string>("TradeItemName")
                        .HasColumnType("text");

                    b.Property<string>("TradeItemProduct")
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

            modelBuilder.Entity("ICL.DWH.Backend.Core.Entities.ProductDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("program_area")
                        .HasColumnType("text");

                    b.Property<string>("trade_item_category")
                        .HasColumnType("text");

                    b.Property<string>("trade_item_code")
                        .HasColumnType("text");

                    b.Property<string>("trade_item_name")
                        .HasColumnType("text");

                    b.Property<string>("trade_item_product")
                        .HasColumnType("text");

                    b.Property<string>("trade_item_product_gs1")
                        .HasColumnType("text");

                    b.Property<string>("trade_item_uid")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("dim_products");
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

                    b.Property<int>("DeliveryStatus")
                        .HasColumnType("integer");

                    b.Property<string>("ErrorMessage")
                        .HasColumnType("text");

                    b.Property<string>("PlaceOfDelivery")
                        .HasColumnType("text");

                    b.Property<string>("PlaceOfReceipt")
                        .HasColumnType("text");

                    b.Property<string>("ProcessType")
                        .HasColumnType("text");

                    b.Property<Guid?>("SCMID")
                        .HasColumnType("uuid");

                    b.Property<string>("SubmitStatus")
                        .HasColumnType("text");

                    b.Property<string>("TransportationMode")
                        .HasColumnType("text");

                    b.Property<Guid?>("shipmentid")
                        .HasColumnType("uuid");

                    b.Property<Guid>("uuid")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("ft_purchase_orders");
                });

            modelBuilder.Entity("ICL.DWH.Backend.Core.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("RoleName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("icl_roles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("26bff42a-c785-47e9-808f-f3a402ac6ee8"),
                            CreateDate = new DateTime(2023, 2, 23, 12, 46, 39, 351, DateTimeKind.Local).AddTicks(388),
                            RoleName = "HQ.User"
                        },
                        new
                        {
                            Id = new Guid("64dbfc18-5508-4af5-aecd-e98cd731fd15"),
                            CreateDate = new DateTime(2023, 2, 23, 12, 46, 39, 351, DateTimeKind.Local).AddTicks(401),
                            RoleName = "Country.User"
                        },
                        new
                        {
                            Id = new Guid("44b8bcf1-f444-4eb0-958e-64c77f24e5c7"),
                            CreateDate = new DateTime(2023, 2, 23, 12, 46, 39, 351, DateTimeKind.Local).AddTicks(404),
                            RoleName = "Washington.User"
                        });
                });

            modelBuilder.Entity("ICL.DWH.Backend.Core.Entities.Statistic", b =>
                {
                    b.Property<DateTime>("createdate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("delivered")
                        .HasColumnType("integer");

                    b.Property<int>("failed")
                        .HasColumnType("integer");

                    b.Property<int>("pending")
                        .HasColumnType("integer");

                    b.Property<string>("processtype")
                        .HasColumnType("text");

                    b.HasKey("createdate");

                    b.ToView("statistics");
                });

            modelBuilder.Entity("ICL.DWH.Backend.Core.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("icl_users");
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
