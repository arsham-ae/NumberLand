using NumberLand.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberLand.DataAccess.DTOs
{
    public class NumberDTO
    {
        public int id { get; set; }
        public string number { get; set; }
        public string application { get; set; }
        public CategoryModel category { get; set; }
        public OperatorModel nOperator { get; set; }
        public DateTime expireTime { get; set; }
        public string price { get; set; }
    }
}
