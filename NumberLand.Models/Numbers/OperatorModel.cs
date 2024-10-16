using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberLand.Models.Numbers
{
    public class OperatorModel
    {
        [Key]
        public int id { get; set; }
        public string slug { get; set; }
        public string operatorCode { get; set; }
        public string country { get; set; }
        public int quantity { get; set; }
    }
}
