using Azure.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NumberLand.DataAccess.Data;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Blogs;
using NumberLand.Utility;

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
