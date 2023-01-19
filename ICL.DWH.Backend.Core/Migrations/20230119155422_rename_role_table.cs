using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ICL.DWH.Backend.Core.Migrations
{
    public partial class rename_role_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "icl_roles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_icl_roles",
                table: "icl_roles",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "icl_roles",
                keyColumn: "Id",
                keyValue: new Guid("26bff42a-c785-47e9-808f-f3a402ac6ee8"),
                column: "CreateDate",
                value: new DateTime(2023, 1, 19, 18, 54, 22, 173, DateTimeKind.Local).AddTicks(7740));

            migrationBuilder.UpdateData(
                table: "icl_roles",
                keyColumn: "Id",
                keyValue: new Guid("44b8bcf1-f444-4eb0-958e-64c77f24e5c7"),
                column: "CreateDate",
                value: new DateTime(2023, 1, 19, 18, 54, 22, 173, DateTimeKind.Local).AddTicks(7761));

            migrationBuilder.UpdateData(
                table: "icl_roles",
                keyColumn: "Id",
                keyValue: new Guid("64dbfc18-5508-4af5-aecd-e98cd731fd15"),
                column: "CreateDate",
                value: new DateTime(2023, 1, 19, 18, 54, 22, 173, DateTimeKind.Local).AddTicks(7757));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_icl_roles",
                table: "icl_roles");

            migrationBuilder.RenameTable(
                name: "icl_roles",
                newName: "Roles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("26bff42a-c785-47e9-808f-f3a402ac6ee8"),
                column: "CreateDate",
                value: new DateTime(2023, 1, 19, 17, 28, 5, 212, DateTimeKind.Local).AddTicks(2672));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("44b8bcf1-f444-4eb0-958e-64c77f24e5c7"),
                column: "CreateDate",
                value: new DateTime(2023, 1, 19, 17, 28, 5, 212, DateTimeKind.Local).AddTicks(2697));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("64dbfc18-5508-4af5-aecd-e98cd731fd15"),
                column: "CreateDate",
                value: new DateTime(2023, 1, 19, 17, 28, 5, 212, DateTimeKind.Local).AddTicks(2693));
        }
    }
}
