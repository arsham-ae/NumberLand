using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NumberLand.DataAccess.Data;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Blogs;
using NumberLand.Models.Numbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberLand.DataAccess.Repository
{
    public class BlogCategoryRepo : Repository<BlogCategoryModel>, IBlogCategoryRepo
    {
        private myDbContext _context;
        public BlogCategoryRepo(myDbContext context) : base(context)
        {
            _context = context;
        }

        public void Patch(int id, [FromBody] JsonPatchDocument<BlogCategoryModel> patchDoc)
        {
            var blogCategory = _context.BlogCategory.FirstOrDefault(p => p.id == id);
            if (blogCategory != null && patchDoc != null)
            {
                patchDoc.ApplyTo(blogCategory);
                _context.SaveChanges();
            }
        }

        public void Update(BlogCategoryModel blogCategory)
        {
            _context.Update(blogCategory);
            _context.SaveChanges();
        }
    }
}
