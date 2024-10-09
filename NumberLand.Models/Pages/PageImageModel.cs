using NumberLand.Models.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberLand.Models.Pages
{
    public class PageImageModel
    {
        public int pageId { get; set; }
        public PageeModel page { get; set; }
        public int imageId { get; set; }
        public ImageModel image { get; set; }
    }
}
