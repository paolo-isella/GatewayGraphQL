using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accounts.Migrations
{
    /// <inheritdoc />
    public partial class Populate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "DateTime", "Username" },
                values: new object[,]
                {
                    {
                        1,
                        "Ada Lovelace",
                        // new DateTime() è DateTime.MinValue
                        new DateTime(1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified),
                        "ada"
                    },
                    {
                        2,
                        "Alan Turing",
                        new DateTime(1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified),
                        "alan-turing"
                    }
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2 }
            );
        }
    }
}
