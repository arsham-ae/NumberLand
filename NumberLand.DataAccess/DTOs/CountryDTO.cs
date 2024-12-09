namespace NumberLand.DataAccess.DTOs
{
    public class CountryDTO
    {
        public int countryId { get; set; }
        public string countrySlug { get; set; }
        public string countryName { get; set; }
        public string countryCode { get; set; }
        public string countryContent { get; set; }
        public string countryFlagIcon { get; set; }
    }
    public class CreateCountryDTO
    {
        public int countryId { get; set; }
        public string countrySlug { get; set; }
        public string countryName { get; set; }
        public string countryCode { get; set; }
        public string? countryContent { get; set; }
    }
}
