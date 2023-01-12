using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ICL.DWH.Backend.Core.Migrations
{
    public partial class update_product_entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProgramArea",
                table: "ft_products",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubmitStatus",
                table: "ft_products",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TradeItemCategory",
                table: "ft_products",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TradeItemName",
                table: "ft_products",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TradeItemProduct",
                table: "ft_products",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProgramArea",
                table: "ft_products");

            migrationBuilder.DropColumn(
                name: "SubmitStatus",
                table: "ft_products");

            migrationBuilder.DropColumn(
                name: "TradeItemCategory",
                table: "ft_products");

            migrationBuilder.DropColumn(
                name: "TradeItemName",
                table: "ft_products");

            migrationBuilder.DropColumn(
                name: "TradeItemProduct",
                table: "ft_products");
        }
    }
}
