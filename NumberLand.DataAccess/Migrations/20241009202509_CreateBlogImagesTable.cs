using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NumberLand.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CreateBlogImagesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlogImage",
                columns: table => new
                {
                    blogId = table.Column<int>(type: "int", nullable: false),
                    imageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogImage", x => new { x.blogId, x.imageId });
                    table.ForeignKey(
                        name: "FK_BlogImage_Blog_blogId",
                        column: x => x.blogId,
                        principalTable: "Blog",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogImage_Image_imageId",
                        column: x => x.imageId,
                        principalTable: "Image",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogImage_imageId",
                table: "BlogImage",
                column: "imageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogImage");
        }
    }
}
