using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberLand.Models.Translation
{
    public class TranslationModel
    {
        [Key]
        public int Id { get; set; }
        public string langCode { get; set; }
        public string key { get; set; }
        public string value { get; set; }
    }
}
