using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace NumberLand.Models.Numbers
{
    public class NumberModel
    {
        [Key]
        public int id { get; set; }
        public string slug { get; set; }
        public string number { get; set; }
        public int applicationId { get; set; }
        [ForeignKey("applicationId")]
        public ApplicationModel application { get; set; }
        public int categoryId { get; set; }
        [ForeignKey("categoryId")]
        public CategoryModel category { get; set; }
        public int operatorId { get; set; }
        [ForeignKey("operatorId")]
        public OperatorModel @operator { get; set; }
        public DateTime? expireTime { get; set; }
    }
}
