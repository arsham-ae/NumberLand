using NumberLand.Models.Blogs;

namespace NumberLand.DataAccess.DTOs
{
    public class BlogDTO
    {
        public int id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public AuthorModel author { get; set; }
        public ICollection<BlogCategoryDTO> blogCategories { get; set; }
        public string featuredImagePath { get; set; }
        public DateTime createAt { get; set; }
        public DateTime updateAt { get; set; }
        public DateTime publishedAt { get; set; }
        public bool isPublished { get; set; }
    }
    public class CreateBlogDTO
    {
        public int id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public int authorId { get; set; }
        public List<int> blogCategories { get; set; }
        public DateTime createAt { get; set; } = DateTime.Now;
        public DateTime updateAt { get; set; }
        public DateTime publishedAt { get; set; }
        public bool isPublished { get; set; }
    }

    public class BlogCategoryDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
}
