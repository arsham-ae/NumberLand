using NumberLand.Models.Blogs;

namespace NumberLand.DataAccess.DTOs
{
    public class BlogDTO
    {
        public int blogId { get; set; }
        public string blogSlug { get; set; }
        public string blogTitle { get; set; }
        public string blogContent { get; set; }
        public AuthorModel blogAuthor { get; set; }
        public ICollection<BlogCategoryDTO> blogCategories { get; set; }
        public string blogFeaturedImagePath { get; set; }
        public DateTime createAt { get; set; }
        public DateTime updateAt { get; set; }
        public DateTime publishedAt { get; set; }
        public bool blogIsPublished { get; set; }
    }
    public class CreateBlogDTO
    {
        public int blogId { get; set; }
        public string blogTitle { get; set; }
        public string blogContent { get; set; }
        public int blogAuthorId { get; set; }
        public List<int> blogCategories { get; set; }
        public DateTime createAt { get; set; } = DateTime.Now;
        public DateTime updateAt { get; set; }
        public DateTime publishedAt { get; set; }
        public bool blogIsPublished { get; set; }
    }
    public class BlogCategoryDTO
    {
        public int blogCategoryId { get; set; }
        public string blogCategorySlug { get; set; }
        public string blogCategoryName { get; set; }
        public string blogCategoryDescription { get; set; }
    }
    public class CreateBlogCategoryDTO
    {
        public int blogCategoryId { get; set; }
        public string blogCategoryName { get; set; }
        public string blogCategoryDescription { get; set; }
    }
    
}
