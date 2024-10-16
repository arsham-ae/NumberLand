using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberLand.Models.Pages
{
    public class PageCategoryModel
    {
        [Key]
        public int id { get; set; }
        public string slug { get; set; }
        public string name { get; set; }
        public int? parentCategoryId { get; set; }
        [ForeignKey("parentCategoryId")]
        public PageCategoryModel parentCategory { get; set; }
    }
}
