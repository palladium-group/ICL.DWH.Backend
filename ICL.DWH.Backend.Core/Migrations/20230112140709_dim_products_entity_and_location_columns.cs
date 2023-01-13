using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ICL.DWH.Backend.Core.Migrations
{
    public partial class dim_products_entity_and_location_columns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PlaceOfDelivery",
                table: "ft_purchase_orders",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlaceOfReceipt",
                table: "ft_purchase_orders",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlaceOfDelivery",
                table: "ft_purchase_orders");

            migrationBuilder.DropColumn(
                name: "PlaceOfReceipt",
                table: "ft_purchase_orders");
        }
    }
}
