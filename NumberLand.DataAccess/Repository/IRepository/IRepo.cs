using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NumberLand.DataAccess.Repository.IRepository
{
    public interface IRepo<T> where T : class
    {
        ICollection<T> GetAll(string? includeProp = null);
        T Get(Expression<Func<T, bool>> filter, string? includeProp = null);
        void Add(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entity);
    }
}
