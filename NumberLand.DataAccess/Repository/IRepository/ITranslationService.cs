using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberLand.DataAccess.Repository.IRepository
{
    public interface ITranslationService
    {
        string GetTranslation(string key, string langCode);
    }
}
