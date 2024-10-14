using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NumberLand.Models.Blogs;
using NumberLand.Models.Numbers;
using NumberLand.Models.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    quantity = 20
                },
                new OperatorModel
                {
                    id = 2,
                    operatorCode = "53",
                    country = "US",
                    quantity = 10
                });

            modelBuilder.Entity<CategoryModel>().HasData(
                new CategoryModel
                {
                    id = 1,
                    name = "Regular",
                    description = ""
                },
                new CategoryModel
                {
                    id = 2,
                    name = "Rental",
                    description = ""
                },
                new CategoryModel
                {
                    id = 3,
                    name = "Permanent",
                    description = ""
                });

            modelBuilder.Entity<BlogCategoryModel>().HasData(
                new BlogCategoryModel
                {
                    id = 1,
                    name = "tech",
                    description = "Heloooo"
                });

        }
    }
}
