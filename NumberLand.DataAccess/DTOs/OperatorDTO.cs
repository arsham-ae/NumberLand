using NumberLand.Models.Numbers;

namespace NumberLand.DataAccess.DTOs
{
    public class OperatorDTO
    {
        public int id { get; set; }
        public string slug { get; set; }
        public string operatorCode { get; set; }
        public CountryModel country { get; set; }
        public int quantity { get; set; }
        public decimal price { get; set; }
    }

    public class CreateOperatorDTO
    {
        public int id { get; set; }
        public string slug { get; set; }
        public string operatorCode { get; set; }
        public int countryId { get; set; }
        public int quantity { get; set; }
        public decimal price { get; set; }
    }
}
