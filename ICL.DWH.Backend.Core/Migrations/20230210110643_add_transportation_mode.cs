using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ICL.DWH.Backend.Core.Migrations
{
    public partial class add_transportation_mode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TransportationMode",
                table: "ft_purchase_orders",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransportationMode",
                table: "ft_purchase_orders");
        }
    }
}
