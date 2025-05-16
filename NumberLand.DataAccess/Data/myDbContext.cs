using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NumberLand.Models.Blogs;
using NumberLand.Models.Numbers;
using NumberLand.Models.Pages;
using NumberLand.Models.Translation;

namespace NumberLand.DataAccess.Data
{
    public class myDbContext : IdentityDbContext<IdentityUser>
    {
        public myDbContext(DbContextOptions options) : base(options)
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
        public DbSet<CountryModel> Country { get; set; }
        public DbSet<ApplicationModel> Application { get; set; }
        public DbSet<TranslationModel> Translation { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OperatorModel>()
        .Property(p => p.price)
        .HasPrecision(18, 2);

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

            modelBuilder.Entity<CategoryModel>().HasData(
                new CategoryModel
                {
                    id = 1,
                    name = "Regular",
                    description = "",
                    slug = "regular"
                },
                new CategoryModel
                {
                    id = 2,
                    name = "Rental",
                    description = "",
                    slug = "rental"
                },
                new CategoryModel
                {
                    id = 3,
                    name = "Permanent",
                    description = "",
                    slug = "permanent"
                });
        }
    }
}
