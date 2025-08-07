using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reviews.Migrations
{
    /// <inheritdoc />
    public partial class Populate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
// Seed Users
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Ada Lovelace" },
                    { 2, "Alan Turing" }
                });

            // Seed Products
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id" },
                values: new object[,]
                {
                    { 1 },
                    { 2 }
                });

            // Seed Reviews
            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Body", "Stars", "UserId", "ProductId" },
                values: new object[,]
                {
                    { 1, "Love it!", 5, 1, 1 },
                    { 2, "Too expensive.", 1, 2, 1 },
                    { 3, "Could be better.", 3, 1, 2 },
                    { 4, "Prefer something else.", 3, 2, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3, 4 });

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2 });
        }
    }
}
