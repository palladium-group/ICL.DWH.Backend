using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ICL.DWH.Backend.Core.Migrations
{
    public partial class create_product_entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "uuid",
                table: "ft_purchase_orders",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ft_products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    uuid = table.Column<Guid>(type: "uuid", nullable: false),
                    PoUuid = table.Column<Guid>(type: "uuid", nullable: false),
                    LineItemId = table.Column<string>(type: "text", nullable: true),
                    ProductCode = table.Column<string>(type: "text", nullable: true),
                    Quantity = table.Column<string>(type: "text", nullable: true),
                    UnitDimension = table.Column<string>(type: "text", nullable: true),
                    UnitVolume = table.Column<string>(type: "text", nullable: true),
                    UnitWeight = table.Column<string>(type: "text", nullable: true),
                    UnitRate = table.Column<string>(type: "text", nullable: true),
                    OrderDetails = table.Column<string>(type: "text", nullable: true),
                    SKULineNo = table.Column<string>(type: "text", nullable: true),
                    PurchaseOrderId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ft_products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ft_products_ft_purchase_orders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "ft_purchase_orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ft_products_PurchaseOrderId",
                table: "ft_products",
                column: "PurchaseOrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ft_products");

            migrationBuilder.DropColumn(
                name: "uuid",
                table: "ft_purchase_orders");
        }
    }
}
