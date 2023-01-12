using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ICL.DWH.Backend.Core.Migrations
{
    public partial class dim_productDetail_entity_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dim_products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    trade_item_id = table.Column<int>(type: "integer", nullable: false),
                    trade_item_code = table.Column<string>(type: "text", nullable: true),
                    trade_item_name = table.Column<string>(type: "text", nullable: true),
                    trade_item_category = table.Column<string>(type: "text", nullable: true),
                    trade_item_product = table.Column<string>(type: "text", nullable: true),
                    program_area = table.Column<string>(type: "text", nullable: true),
                    submit_status = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dim_products", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dim_products");
        }
    }
}
