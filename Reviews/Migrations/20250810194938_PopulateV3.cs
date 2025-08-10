using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reviews.Migrations
{
    /// <inheritdoc />
    public partial class PopulateV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Seed LifeCycles
            migrationBuilder.InsertData(
                table: "LifeCycles",
                columns: new[] { "Id", "StartDate", "EndDate", "ReviewId" },
                values: new object[,]
                {
                    { 1, new DateTime(2020,1,1), DateTime.Now, 1 },
                    { 2, DateTime.Now, DateTime.MaxValue, 1 },
                    { 3, new DateTime(2020,1,1), DateTime.Now, 2 },
                    { 4, DateTime.Now, DateTime.MaxValue, 2 },
                    { 5, new DateTime(2020,1,1), DateTime.Now, 3 },
                    { 6, DateTime.Now, DateTime.MaxValue, 3 },
                    { 7, new DateTime(2020,1,1), DateTime.Now, 4 },
                    { 8, DateTime.Now, DateTime.MaxValue, 4 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3, 4, 5, 6, 7, 8 });
        }
    }
}
