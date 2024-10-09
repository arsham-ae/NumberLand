using NumberLand.Models.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberLand.Models.Blogs
{
    public class BlogImageModel
    {
        public int blogId { get; set; }
        public BlogModel blog { get; set; }
        public int imageId { get; set; }
        public ImageModel image { get; set; }
    }
}
