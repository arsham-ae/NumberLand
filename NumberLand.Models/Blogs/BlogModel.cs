using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberLand.Models.Blogs
{
    public class BlogModel
    {
        [Key]
        public int id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public int authorId { get; set; }
        [ForeignKey("authorId")]
        public AuthorModel author { get; set; }
        public ICollection<BlogCategoryJoinModel> blogCategories { get; set; }
        public DateTime createAt { get; set; } = DateTime.Now;
        public DateTime updateAt { get; set; }
        public DateTime publishedAt { get; set; }
        public ICollection<BlogImageModel> blogImage { get; set; }
        public bool isPublished { get; set; }

    }
}
