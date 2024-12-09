using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NumberLand.DataAccess.Data;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Numbers;

namespace NumberLand.DataAccess.Repository
{
    public class ApplicationRepo : Repository<ApplicationModel>, IApplicationRepo
    {
        private myDbContext _context;
        public ApplicationRepo(myDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task Patch(int id, [FromBody] JsonPatchDocument<ApplicationModel> patchDoc)
        {
            var application = await _context.Application.FirstOrDefaultAsync(p => p.id == id);
            if (application != null && patchDoc != null)
            {
                patchDoc.ApplyTo(application);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Update(ApplicationModel application)
        {
            _context.Update(application);
            await _context.SaveChangesAsync();
        }
    }
}
