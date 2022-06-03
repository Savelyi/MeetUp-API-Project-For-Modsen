using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class EditedEventConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "54437ad7-408c-42bd-b01f-58f3c122009c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8a2ecae4-c970-4689-a0da-31aeaa9e3815");

            migrationBuilder.AlterColumn<string>(
                name: "OrganizerId",
                table: "Events",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c81b7088-dea9-4174-b6eb-4d5cd354ff3e", "45d4dc3d-eed6-4185-b8b4-b2d4f0aa6e5d", "Client", "CLIENT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "64bea381-f973-4711-87ab-3de0aa83a778", "fd116014-1496-43bd-a5e4-7e3dfbaed295", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "64bea381-f973-4711-87ab-3de0aa83a778");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c81b7088-dea9-4174-b6eb-4d5cd354ff3e");

            migrationBuilder.AlterColumn<string>(
                name: "OrganizerId",
                table: "Events",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "54437ad7-408c-42bd-b01f-58f3c122009c", "2a9f0864-54ff-4c12-9c6a-a983b2248216", "Client", "CLIENT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8a2ecae4-c970-4689-a0da-31aeaa9e3815", "420e9811-9fc1-44d1-97d6-55d1dd4d9d7e", "Admin", "ADMIN" });
        }
    }
}
