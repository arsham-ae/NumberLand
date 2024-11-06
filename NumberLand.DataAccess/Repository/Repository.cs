using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NumberLand.DataAccess.Data;
using NumberLand.DataAccess.Repository.IRepository;
using System.Linq.Expressions;
using System.Net.WebSockets;

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
        public async Task<IEnumerable<T>> GetAll(string? includeProp = null)
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
            return await query.ToListAsync();
        }
        public async Task<T> Get(Expression<Func<T, bool>> filter, string? includeProp = null)
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
            return await query.FirstOrDefaultAsync();
        }
        public async Task Add(T entity)
        {
            await dbSet.AddAsync(entity);
        }
        public async Task UpdateAsync(T entity)
        {
            dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
        public async Task PatchAsync(int id, [FromBody] JsonPatchDocument<T> patchDoc)
        {
            var entity = await dbSet.FindAsync(id);
            patchDoc.ApplyTo(entity);
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