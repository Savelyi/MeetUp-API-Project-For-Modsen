using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class EditedUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "177e87a0-22ff-4988-a3b7-e44f984b93d3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "acf67113-fbcd-4bc4-88ce-4fd9d4872ee0");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "20077bb6-6e19-439d-90f6-d6e5beb5ad76", "c22e1920-29b8-4c92-ae47-b20ba31a1e32", "Client", "CLIENT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0fd0d0d5-becd-4ab4-91e7-a29e1308aa91", "77e43d0e-cb8a-4241-833d-41c71a304f8c", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0fd0d0d5-becd-4ab4-91e7-a29e1308aa91");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "20077bb6-6e19-439d-90f6-d6e5beb5ad76");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "acf67113-fbcd-4bc4-88ce-4fd9d4872ee0", "b72c148e-7772-4e4c-9988-166b78fd799f", "Client", "CLIENT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "177e87a0-22ff-4988-a3b7-e44f984b93d3", "1422f7c2-8813-4ed6-8da0-db56233433c0", "Admin", "ADMIN" });
        }
    }
}
