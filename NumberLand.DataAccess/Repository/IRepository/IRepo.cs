using System.Linq.Expressions;

namespace NumberLand.DataAccess.Repository.IRepository
{
    public interface IRepo<T> where T : class
    {
        Task<IEnumerable<T>> GetAll(string? includeProp = null);
        Task<T> Get(Expression<Func<T, bool>> filter, string? includeProp = null);
        Task Add(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entity);
    }
}
