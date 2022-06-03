using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class DeletingRoleConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "64bea381-f973-4711-87ab-3de0aa83a778");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c81b7088-dea9-4174-b6eb-4d5cd354ff3e");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c81b7088-dea9-4174-b6eb-4d5cd354ff3e", "45d4dc3d-eed6-4185-b8b4-b2d4f0aa6e5d", "Client", "CLIENT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "64bea381-f973-4711-87ab-3de0aa83a778", "fd116014-1496-43bd-a5e4-7e3dfbaed295", "Admin", "ADMIN" });
        }
    }
}
