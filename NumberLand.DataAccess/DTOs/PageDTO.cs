using NumberLand.Models.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberLand.DataAccess.DTOs
{
    public class PageDTO
    {
        public int id { get; set; }
        public string slug { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public PageCategoryModel category { get; set; }
        public bool isVisible { get; set; }
    }
    public class CreatePageDTO
    {
        public int id { get; set; }
        public string slug { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public int categoryId { get; set; }
        public bool isVisible { get; set; }
    }

    public class PageCategoryDTO
    {
        public int id { get; set; }
        public string slug { get; set; }
        public string name { get; set; }
        public PageCategoryModel parentCategory { get; set; }
    }

    public class CreatePageCategoryDTO
    {
        public int id { get; set; }
        public string slug { get; set; }
        public string name { get; set; }
        public int? parentCategoryId { get; set; }
    }
}
