using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberLand.DataAccess.Helper
{
    public class QueryObject
    {
        public string? author { get; set; } = null;
        public string? blogCategory { get; set; } = null;
        public string? blogSlug { get; set; } = null;
        public int pageNumber { get; set; } = 1;
        public int limit { get; set; } = 9;
    }
}
