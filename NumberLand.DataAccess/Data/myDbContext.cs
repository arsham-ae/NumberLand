using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NumberLand.Models.Blogs;
using NumberLand.Models.Images;
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
        public DbSet<ImageModel> Image { get; set; }
        public DbSet<PageImageModel> PageImage { get; set; }
        public DbSet<BlogModel> Blog { get; set; }
        public DbSet<BlogCategoryModel> BlogCategory { get; set; }
        public DbSet<BlogCategoryJoinModel> BlogCategoryJoin { get; set; }
        public DbSet<BlogImageModel> BlogImage { get; set; }
        public DbSet<AuthorModel> Author { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PageImageModel>()
                .HasKey(pi => new { pi.pageId, pi.imageId });

            modelBuilder.Entity<PageImageModel>()
                .HasOne(pi => pi.page)
                .WithMany(p => p.pageImage)
                .HasForeignKey(pi => pi.pageId);

            modelBuilder.Entity<PageImageModel>()
                .HasOne(pi => pi.image)
                .WithMany(i => i.pageImage)
                .HasForeignKey(pi => pi.imageId);


            modelBuilder.Entity<BlogImageModel>()
                .HasKey(bi => new { bi.blogId, bi.imageId });

            modelBuilder.Entity<BlogImageModel>()
                .HasOne(bi => bi.blog)
                .WithMany(b => b.blogImage)
                .HasForeignKey(bi => bi.blogId);

            modelBuilder.Entity<BlogImageModel>()
                .HasOne(bi => bi.image)
                .WithMany(i => i.blogImage)
                .HasForeignKey(bi => bi.imageId);


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
                    name = "Regular"
                },
                new CategoryModel
                {
                    id = 2,
                    name = "Rental"
                },
                new CategoryModel
                {
                    id = 3,
                    name = "Permanent"
                });

        }
    }
}
