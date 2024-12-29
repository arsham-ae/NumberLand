using System.Text.RegularExpressions;

namespace NumberLand.Utility
{
    public class SlugHelper
    {
        public static string GenerateSlug(string text)
        {
            if (text.Any(c => c >= '\u0600' && c <= '\u06FF'))
            {
                throw new Exception("Slug cannot contain Persian characters.");
            }
            string slug = text.ToLower().TrimStart().TrimEnd();
            slug = slug.Replace(' ', '-');
            slug = Regex.Replace(slug, @"[^a-zA-Z0-9]", "");
            slug = Regex.Replace(slug, @"-+", "-");
            slug = $"{slug}-{DateTime.UtcNow.ToString("yyyyMMddHHmmss")}";
            return slug;
        }
    }
}
