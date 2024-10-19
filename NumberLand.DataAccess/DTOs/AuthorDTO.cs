using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberLand.DataAccess.DTOs
{
    public class AuthorDTO
    {
        public int id { get; set; }
        public string slug { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
}
