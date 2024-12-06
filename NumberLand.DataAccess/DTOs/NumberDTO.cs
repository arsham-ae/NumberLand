using NumberLand.Models.Numbers;

namespace NumberLand.DataAccess.DTOs
{
    public class NumberDTO
    {
        public int id { get; set; }
        public string numberSlug { get; set; }
        public string number { get; set; }
        public ApplicationModel application { get; set; }
        public CategoryModel category { get; set; }
        public OperatorModel @operator { get; set; }
        public DateTime expireTime { get; set; }
    }
    public class CreateNumberDTO
    {
        public int id { get; set; }
        public string numberSlug { get; set; }
        public string number { get; set; }
        public int applicationId { get; set; }
        public int categoryId { get; set; }
        public int operatorId { get; set; }
        public DateTime? expireTime { get; set; }
    }
}
