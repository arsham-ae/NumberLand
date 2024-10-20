using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NumberLand.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CreateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    imagePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "BlogCategory",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogCategory", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Operator",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    operatorCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operator", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PageCategory",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    parentCategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageCategory", x => x.id);
                    table.ForeignKey(
                        name: "FK_PageCategory_PageCategory_parentCategoryId",
                        column: x => x.parentCategoryId,
                        principalTable: "PageCategory",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Blog",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    authorId = table.Column<int>(type: "int", nullable: false),
                    featuredImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    publishedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isPublished = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog", x => x.id);
                    table.ForeignKey(
                        name: "FK_Blog_Author_authorId",
                        column: x => x.authorId,
                        principalTable: "Author",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Numbers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    application = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    categoryId = table.Column<int>(type: "int", nullable: false),
                    operatorId = table.Column<int>(type: "int", nullable: false),
                    expireTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    price = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Numbers", x => x.id);
                    table.ForeignKey(
                        name: "FK_Numbers_Category_categoryId",
                        column: x => x.categoryId,
                        principalTable: "Category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Numbers_Operator_operatorId",
                        column: x => x.operatorId,
                        principalTable: "Operator",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Page",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    categoryId = table.Column<int>(type: "int", nullable: false),
                    isVisible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Page", x => x.id);
                    table.ForeignKey(
                        name: "FK_Page_PageCategory_categoryId",
                        column: x => x.categoryId,
                        principalTable: "PageCategory",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlogCategoryJoin",
                columns: table => new
                {
                    blogId = table.Column<int>(type: "int", nullable: false),
                    categoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogCategoryJoin", x => new { x.blogId, x.categoryId });
                    table.ForeignKey(
                        name: "FK_BlogCategoryJoin_BlogCategory_categoryId",
                        column: x => x.categoryId,
                        principalTable: "BlogCategory",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogCategoryJoin_Blog_blogId",
                        column: x => x.blogId,
                        principalTable: "Blog",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Regular" },
                    { 2, "Rental" },
                    { 3, "Permanent" }
                });

            migrationBuilder.InsertData(
                table: "Operator",
                columns: new[] { "id", "country", "operatorCode", "quantity" },
                values: new object[,]
                {
                    { 1, "UK", "43", 20 },
                    { 2, "US", "53", 10 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blog_authorId",
                table: "Blog",
                column: "authorId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogCategoryJoin_categoryId",
                table: "BlogCategoryJoin",
                column: "categoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Numbers_categoryId",
                table: "Numbers",
                column: "categoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Numbers_operatorId",
                table: "Numbers",
                column: "operatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Page_categoryId",
                table: "Page",
                column: "categoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PageCategory_parentCategoryId",
                table: "PageCategory",
                column: "parentCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogCategoryJoin");

            migrationBuilder.DropTable(
                name: "Numbers");

            migrationBuilder.DropTable(
                name: "Page");

            migrationBuilder.DropTable(
                name: "BlogCategory");

            migrationBuilder.DropTable(
                name: "Blog");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Operator");

            migrationBuilder.DropTable(
                name: "PageCategory");

            migrationBuilder.DropTable(
                name: "Author");
        }
    }
}
