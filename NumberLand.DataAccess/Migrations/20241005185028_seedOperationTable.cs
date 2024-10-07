using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NumberLand.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class seedOperationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.InsertData(
                table: "Operator",
                columns: new[] { "id", "country", "operatorCode", "quantity" },
                values: new object[,]
                {
                    { 1, "UK", "43", 20 },
                    { 2, "US", "53", 10 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Operator",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Operator",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Regular" },
                    { 2, "Rental" },
                    { 3, "Permanent" }
                });
        }
    }
}
