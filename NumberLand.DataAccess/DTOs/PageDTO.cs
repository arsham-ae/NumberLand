using NumberLand.Models.Pages;

namespace NumberLand.DataAccess.DTOs
{
    public class PageDTO
    {
        public int id { get; set; }
        public string pageSlug { get; set; }
        public string pageTitle { get; set; }
        public string pageContent { get; set; }
        public PageCategoryModel pageCategory { get; set; }
        public bool PageIsVisible { get; set; }
    }
    public class CreatePageDTO
    {
        public int id { get; set; }
        public string pageSlug { get; set; }
        public string pageTitle { get; set; }
        public string pageContent { get; set; }
        public int pageCategoryId { get; set; }
        public bool PageIsVisible { get; set; }
    }

    public class PageCategoryDTO
    {
        public int id { get; set; }
        public string pageCategorySlug { get; set; }
        public string pageCategoryName { get; set; }
        public PageCategoryModel pageCategoryParentCategory { get; set; }
    }

    public class CreatePageCategoryDTO
    {
        public int id { get; set; }
        public string pageCategorySlug { get; set; }
        public string pageCategoryName { get; set; }
        public int? pageCategoryParentCategoryId { get; set; }
    }
}
