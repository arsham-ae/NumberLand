using System.ComponentModel.DataAnnotations;

namespace NumberLand.Models.Blogs
{
    public class AuthorModel
    {
        [Key]
        public int id { get; set; }
        public string slug { get; set; }
        public string name { get; set; }
        public string? description { get; set; }
        public string imagePath { get; set; }
    }
}
