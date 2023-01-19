using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ICL.DWH.Backend.Core.Migrations
{
    public partial class seed_data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "RoleName" },
                values: new object[,]
                {
                    { new Guid("26bff42a-c785-47e9-808f-f3a402ac6ee8"), new DateTime(2023, 1, 19, 17, 28, 5, 212, DateTimeKind.Local).AddTicks(2672), "HQ.User" },
                    { new Guid("44b8bcf1-f444-4eb0-958e-64c77f24e5c7"), new DateTime(2023, 1, 19, 17, 28, 5, 212, DateTimeKind.Local).AddTicks(2697), "Washington.User" },
                    { new Guid("64dbfc18-5508-4af5-aecd-e98cd731fd15"), new DateTime(2023, 1, 19, 17, 28, 5, 212, DateTimeKind.Local).AddTicks(2693), "Country.User" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("26bff42a-c785-47e9-808f-f3a402ac6ee8"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("44b8bcf1-f444-4eb0-958e-64c77f24e5c7"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("64dbfc18-5508-4af5-aecd-e98cd731fd15"));
        }
    }
}
