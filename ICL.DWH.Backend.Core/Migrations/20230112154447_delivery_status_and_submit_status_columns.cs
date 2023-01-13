using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ICL.DWH.Backend.Core.Migrations
{
    public partial class delivery_status_and_submit_status_columns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "ft_purchase_orders",
                newName: "DeliveryStatus");

            migrationBuilder.AddColumn<string>(
                name: "SubmitStatus",
                table: "ft_purchase_orders",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductGS1Code",
                table: "ft_products",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubmitStatus",
                table: "ft_purchase_orders");

            migrationBuilder.DropColumn(
                name: "ProductGS1Code",
                table: "ft_products");

            migrationBuilder.DropColumn(
                name: "trade_item_product_gs1",
                table: "dim_products");

            migrationBuilder.RenameColumn(
                name: "DeliveryStatus",
                table: "ft_purchase_orders",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "trade_item_uid",
                table: "dim_products",
                newName: "submit_status");

            migrationBuilder.AddColumn<int>(
                name: "trade_item_id",
                table: "dim_products",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
