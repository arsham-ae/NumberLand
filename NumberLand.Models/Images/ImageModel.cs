using Microsoft.AspNetCore.Mvc.RazorPages;
using NumberLand.Models.Blogs;
using NumberLand.Models.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberLand.Models.Images
{
    public class ImageModel
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string imagePath { get; set; }
        public ICollection<PageImageModel> pageImage { get; set; }
        public ICollection<BlogImageModel> blogImage { get; set; }

    }
}
