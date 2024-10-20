using Microsoft.EntityFrameworkCore;
using NumberLand.Models.Blogs;
using NumberLand.Models.Numbers;
using NumberLand.Models.Pages;

namespace NumberLand.DataAccess.Data
{
    public class myDbContext : DbContext
    {
        public myDbContext(DbContextOptions<myDbContext> options) : base(options)
        {

        }
        public DbSet<NumberModel> Numbers { get; set; }
        public DbSet<CategoryModel> Category { get; set; }
        public DbSet<OperatorModel> Operator { get; set; }
        public DbSet<PageeModel> Page { get; set; }
        public DbSet<PageCategoryModel> PageCategory { get; set; }
        public DbSet<BlogModel> Blog { get; set; }
        public DbSet<BlogCategoryModel> BlogCategory { get; set; }
        public DbSet<BlogCategoryJoinModel> BlogCategoryJoin { get; set; }
        public DbSet<AuthorModel> Author { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BlogCategoryJoinModel>()
                .HasKey(bc => new { bc.blogId, bc.categoryId });

            modelBuilder.Entity<BlogCategoryJoinModel>()
                .HasOne(bc => bc.blog)
                .WithMany(b => b.blogCategories)
                .HasForeignKey(bc => bc.blogId);

            modelBuilder.Entity<BlogCategoryJoinModel>()
                .HasOne(bc => bc.category)
                .WithMany(c => c.blogCategories)
                .HasForeignKey(bc => bc.categoryId);







            modelBuilder.Entity<OperatorModel>().HasData(
                new OperatorModel
                {
                    id = 1,
                    operatorCode = "43",
                    country = "UK",
                    quantity = 20,
                    slug = "eg"
                },
                new OperatorModel
                {
                    id = 2,
                    operatorCode = "53",
                    country = "US",
                    quantity = 10,
                    slug = "asd"
                });

            modelBuilder.Entity<CategoryModel>().HasData(
                new CategoryModel
                {
                    id = 1,
                    name = "Regular",
                    description = "",
                    slug = "ed"
                },
                new CategoryModel
                {
                    id = 2,
                    name = "Rental",
                    description = "",
                    slug = "ss"
                },
                new CategoryModel
                {
                    id = 3,
                    name = "Permanent",
                    description = "",
                    slug = "sf"
                });

            modelBuilder.Entity<BlogCategoryModel>().HasData(
                new BlogCategoryModel
                {
                    id = 1,
                    name = "tech",
                    description = "Heloooo",
                    slug = "a"
                });

        }
    }
}
