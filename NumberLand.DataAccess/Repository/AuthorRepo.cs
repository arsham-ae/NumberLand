using Microsoft.EntityFrameworkCore;
using NumberLand.DataAccess.Data;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Blogs;

namespace NumberLand.DataAccess.Repository
{
    public class AuthorRepo : Repository<AuthorModel>, IAuthorRepo
    {
        private readonly myDbContext _context;
        public AuthorRepo(myDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task Update(AuthorModel author)
        {
            _context.Attach(author);
            _context.Entry(author).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
