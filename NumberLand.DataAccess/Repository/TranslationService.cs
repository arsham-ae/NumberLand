using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NumberLand.DataAccess.Data;
using NumberLand.DataAccess.Repository.IRepository;

namespace NumberLand.DataAccess.Repository
{
    public class TranslationService : ITranslationService
    {
        private readonly myDbContext _context;

        public TranslationService(myDbContext context)
        {
            _context = context;
        }

        public string GetTranslation(string key, string langCode)
        {
            var translation = _context.Translation
                .Where(t => t.key == key && t.langCode == langCode)
                .Select(t => t.value)
                .FirstOrDefault() ?? key;
            return translation;
        }
    }
}
