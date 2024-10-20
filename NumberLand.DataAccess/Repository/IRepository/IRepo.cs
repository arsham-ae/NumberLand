using System.Linq.Expressions;

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
