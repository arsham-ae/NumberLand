using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NumberLand.DataAccess.Data;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Pages;
using NumberLand.Utility;

namespace NumberLand.DataAccess.Repository
{
    public class PageCategoryRepo : Repository<PageCategoryModel>, IPageCategoryRepo
    {
        private myDbContext _context;
        public PageCategoryRepo(myDbContext context) : base(context)
        {
            _context = context;
        }

        public async void Patch(int id, [FromBody] JsonPatchDocument<PageCategoryModel> patchDoc)
        {
            var pageCategory = await _context.PageCategory.FirstOrDefaultAsync(p => p.id == id);
            if (pageCategory != null && patchDoc != null)
            {
                patchDoc.ApplyTo(pageCategory);
                pageCategory.slug = SlugHelper.GenerateSlug(pageCategory.name);
                await _context.SaveChangesAsync();
            }
        }

        public void Update(PageCategoryModel pageCategory)
        {
            _context.Update(pageCategory);
            _context.SaveChanges();
        }
    }
}
