using Microsoft.EntityFrameworkCore;
using NumberLand.Models;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
