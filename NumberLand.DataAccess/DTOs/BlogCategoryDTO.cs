using NumberLand.Models.Blogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberLand.DataAccess.DTOs
{
    public class BlogCategoryDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public BlogCategoryDTO(BlogCategoryModel blogCategory)
        {
            id = blogCategory.id;
            name = blogCategory.name;
            description = blogCategory.description;
        }
    }
}
