using NumberLand.Models.Blogs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberLand.DataAccess.DTOs
{
    public class CreateBlogDTO
    {
        public int id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public int authorId { get; set; }
        public List<int> blogCategories { get; set; }
        public string featuredImagePath { get; set; }
        public DateTime createAt { get; set; } = DateTime.Now;
        public DateTime updateAt { get; set; }
        public DateTime publishedAt { get; set; }
        public bool isPublished { get; set; }
    }
}
