using Microsoft.EntityFrameworkCore;
using NumberLand.DataAccess.Data;
using NumberLand.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NumberLand.DataAccess.Repository
{
    public class Repository<T> : IRepo<T> where T : class
    {
        private readonly myDbContext _context;
        internal DbSet<T> dbSet;
        public Repository(myDbContext context)
        {
            _context = context;
            this.dbSet = _context.Set<T>();
        }
        public ICollection<T> GetAll(string? includeProp = null)
        {
            IQueryable<T> query = dbSet;
            if (!string.IsNullOrEmpty(includeProp))
            {
                foreach (var prop in includeProp
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(prop.Trim());
                }
            }
            return query.ToList();
        }
        public T Get(Expression<Func<T, bool>> filter, string? includeProp = null)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProp))
            {
                foreach (var prop in includeProp
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(prop.Trim());
                }
            }
            return query.FirstOrDefault();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }
        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }
    }
}
