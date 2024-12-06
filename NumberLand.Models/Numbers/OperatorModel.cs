using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NumberLand.Models.Numbers
{
    public class OperatorModel
    {
        [Key]
        public int id { get; set; }
        public string slug { get; set; }
        public string operatorCode { get; set; }
        public int countryId { get; set; }
        [ForeignKey("countryId")]
        public CountryModel country { get; set; }
        public int quantity { get; set; }
        public decimal price { get; set; }
    }
}
