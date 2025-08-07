using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reviews.Migrations
{
    /// <inheritdoc />
    public partial class PopulateV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}