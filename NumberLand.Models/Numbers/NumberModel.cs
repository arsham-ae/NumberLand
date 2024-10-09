using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace NumberLand.Models.Numbers
{
    public class NumberModel
    {
        [Key]
        public int id { get; set; }
        public string number { get; set; }
        public string application { get; set; }
        public int categoryId { get; set; }
        [ForeignKey("categoryId")]
        public CategoryModel category { get; set; }
        public int operatorId { get; set; }
        [ForeignKey("operatorId")]
        [DisplayName("Operator")]
        public OperatorModel nOperator { get; set; }
        public DateTime expireTime { get; set; }
        public string price { get; set; }
    }
}
