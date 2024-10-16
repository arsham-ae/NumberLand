using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NumberLand.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addSlugToTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "slug",
                table: "PageCategory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "slug",
                table: "Page",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "slug",
                table: "Operator",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "slug",
                table: "Numbers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "slug",
                table: "Category",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "slug",
                table: "BlogCategory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "slug",
                table: "Blog",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "slug",
                table: "Author",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "BlogCategory",
                keyColumn: "id",
                keyValue: 1,
                column: "slug",
                value: "a");

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "id",
                keyValue: 1,
                column: "slug",
                value: "ed");

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "id",
                keyValue: 2,
                column: "slug",
                value: "ss");

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "id",
                keyValue: 3,
                column: "slug",
                value: "sf");

            migrationBuilder.UpdateData(
                table: "Operator",
                keyColumn: "id",
                keyValue: 1,
                column: "slug",
                value: "eg");

            migrationBuilder.UpdateData(
                table: "Operator",
                keyColumn: "id",
                keyValue: 2,
                column: "slug",
                value: "asd");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "slug",
                table: "PageCategory");

            migrationBuilder.DropColumn(
                name: "slug",
                table: "Page");

            migrationBuilder.DropColumn(
                name: "slug",
                table: "Operator");

            migrationBuilder.DropColumn(
                name: "slug",
                table: "Numbers");

            migrationBuilder.DropColumn(
                name: "slug",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "slug",
                table: "BlogCategory");

            migrationBuilder.DropColumn(
                name: "slug",
                table: "Blog");

            migrationBuilder.DropColumn(
                name: "slug",
                table: "Author");
        }
    }
}
