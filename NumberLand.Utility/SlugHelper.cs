using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NumberLand.Utility
{
    public class SlugHelper
    {
        public static string GenerateSlug(string text)
        {
            string slug = text.ToLower().Trim();
            slug = slug.Replace(' ', '-');
            slug = Regex.Replace(slug, @"[^a-zA-Z0-9\u0600-\u06FF-]", "");
            slug = Regex.Replace(slug, @"-+", "-");
            return slug;
        }
    }
}
