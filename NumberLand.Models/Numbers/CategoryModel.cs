using System.ComponentModel.DataAnnotations;

namespace NumberLand.Models.Numbers
{
    public class CategoryModel
    {
        [Key]
        public int id { get; set; }
        public string slug { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
}
