using System.Text.RegularExpressions;

namespace NumberLand.Utility
{
    public class SlugHelper
    {
        public static string GenerateSlug(string text)
        {
            string slug = text.ToLower().Trim();
            slug = slug.Replace(' ', '-');
            slug = Regex.Replace(slug, @"[^a-zA-Z0-9]", "");
            slug = Regex.Replace(slug, @"-+", "-");
            slug = $"{slug}-{DateTime.UtcNow.ToString("yyyyMMddHHmmss")}";
            return slug;
        }
    }
}
