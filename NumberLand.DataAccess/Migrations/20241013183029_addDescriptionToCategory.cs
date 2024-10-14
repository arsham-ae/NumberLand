using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NumberLand.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addDescriptionToCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "Category",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "id",
                keyValue: 1,
                column: "description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "id",
                keyValue: 2,
                column: "description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "id",
                keyValue: 3,
                column: "description",
                value: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "Category");
        }
    }
}
