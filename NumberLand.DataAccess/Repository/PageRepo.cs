using Markdig;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NumberLand.DataAccess.Data;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Pages;
using NumberLand.Utility;

namespace NumberLand.DataAccess.Repository
{
    public class PageRepo : Repository<PageeModel>, IPageRepo
    {
        private myDbContext _context;
        public PageRepo(myDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task Patch(int id, [FromBody] JsonPatchDocument<PageeModel> patchDoc)
        {
            var page = await _context.Page.FirstOrDefaultAsync(p => p.id == id);
            if (page != null && patchDoc != null)
            {
                patchDoc.ApplyTo(page);
                page.slug = SlugHelper.GenerateSlug2(page.title);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Update(PageeModel page)
        {
            _context.Update(page);
            await _context.SaveChangesAsync();
        }
    }
}
