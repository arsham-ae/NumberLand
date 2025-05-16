using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NumberLand.DataAccess.Helper
{
    public class LanguageHelper
    {
        public static string GetLanguage(HttpContext httpContext)
        {
            var lang = httpContext.Request.Headers["Accept-Language"].FirstOrDefault();
            return string.IsNullOrWhiteSpace(lang) ? "fa" : lang;
        }
    }
}
