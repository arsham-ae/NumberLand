using System.ComponentModel.DataAnnotations;

namespace NumberLand.Models.Numbers
{
    public class CountryModel
    {
        [Key]
        public int id { get; set; }
        public string slug { get; set; }
        public string name { get; set; }
        public string countryCode { get; set; }
        public string? content { get; set; }
        public string flagIcon { get; set; }
    }
}
