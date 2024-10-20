using System.ComponentModel.DataAnnotations;

namespace NumberLand.Models.Blogs
{
    public class BlogCategoryModel
    {
        [Key]
        public int id { get; set; }
        public string slug { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public ICollection<BlogCategoryJoinModel> blogCategories { get; set; }
    }
}
