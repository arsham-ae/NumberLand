using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NumberLand.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class seedBlogCategoryTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BlogCategory",
                columns: new[] { "id", "description", "name" },
                values: new object[] { 1, "Heloooo", "tech" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BlogCategory",
                keyColumn: "id",
                keyValue: 1);
        }
    }
}
