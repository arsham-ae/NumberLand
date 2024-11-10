using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NumberLand.DataAccess.Data;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Blogs;

namespace NumberLand.DataAccess.Repository
{
    public class BlogCategoryRepo : Repository<BlogCategoryModel>, IBlogCategoryRepo
    {
        private myDbContext _context;
        public BlogCategoryRepo(myDbContext context) : base(context)
        {
            _context = context;
        }

        public async void Patch(int id, [FromBody] JsonPatchDocument<BlogCategoryModel> patchDoc)
        {
            var blogCategory = await _context.BlogCategory.FirstOrDefaultAsync(p => p.id == id);
            if (blogCategory != null && patchDoc != null)
            {
                patchDoc.ApplyTo(blogCategory);
                await _context.SaveChangesAsync();
            }
            if (blogCategory != null || patchDoc == null)
            {
                throw new KeyNotFoundException("Invalid Data!");
            }
        }

        public void Update(BlogCategoryModel blogCategory)
        {
            _context.Update(blogCategory);
            _context.SaveChanges();
        }
    }
}
