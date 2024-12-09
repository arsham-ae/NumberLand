using System.ComponentModel.DataAnnotations;

namespace NumberLand.Models.Numbers
{
    public class ApplicationModel
    {
        [Key]
        public int id { get; set; }
        public string slug { get; set; }
        public string name { get; set; }
        public string? content { get; set; }
        public string appIcon { get; set; }

    }
}
