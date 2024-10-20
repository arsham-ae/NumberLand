namespace NumberLand.Models.Blogs
{
    public class BlogCategoryJoinModel
    {
        public int blogId { get; set; }
        public BlogModel blog { get; set; }
        public int categoryId { get; set; }
        public BlogCategoryModel category { get; set; }
    }
}
