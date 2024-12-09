using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NumberLand.DataAccess.Data;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Numbers;

namespace NumberLand.DataAccess.Repository
{
    public class CategoryRepo : Repository<CategoryModel>, ICategoryRepo
    {
        private myDbContext _context;
        public CategoryRepo(myDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task Patch(int id, [FromBody] JsonPatchDocument<CategoryModel> patchDoc)
        {
            var category = await _context.Category.FirstOrDefaultAsync(p => p.id == id);
            if (category != null && patchDoc != null)
            {
                patchDoc.ApplyTo(category);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Update(CategoryModel category)
        {
            _context.Update(category);
            await _context.SaveChangesAsync();
        }
    }
}
