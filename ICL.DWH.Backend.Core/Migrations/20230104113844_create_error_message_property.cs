using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ICL.DWH.Backend.Core.Migrations
{
    public partial class create_error_message_property : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ErrorMessage",
                table: "ft_purchase_orders",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ErrorMessage",
                table: "ft_purchase_orders");
        }
    }
}
