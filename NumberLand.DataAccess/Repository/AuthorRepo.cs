using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NumberLand.DataAccess.Data;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Blogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberLand.DataAccess.Repository
{
    public class AuthorRepo : Repository<AuthorModel>, IAuthorRepo
    {
        private myDbContext _context;
        public AuthorRepo(myDbContext context) : base(context)
        {
            _context = context;
        }

        public void Patch(int id, [FromBody] JsonPatchDocument<AuthorModel> patchDoc)
        {
            var author = _context.Author.FirstOrDefault(p => p.id == id);
            if (author != null && patchDoc != null)
            {
                patchDoc.ApplyTo(author);
                _context.SaveChanges();
            }
        }

        public void Update(AuthorModel author)
        {
            _context.Update(author);
            _context.SaveChanges();
        }
    }
}
