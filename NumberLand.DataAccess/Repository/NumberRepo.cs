using Azure.Core;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NumberLand.DataAccess.Data;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Numbers;
using NumberLand.Utility;

namespace NumberLand.DataAccess.Repository
{
    public class NumberRepo : Repository<NumberModel>, INumberRepo
    {
        private myDbContext _context;
        public NumberRepo(myDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task Patch(int id, [FromBody] JsonPatchDocument<NumberModel> patchDoc)
        {
            var number = await _context.Numbers.FirstOrDefaultAsync(p => p.id == id);
            if (number != null && patchDoc != null)
            {
                patchDoc.ApplyTo(number);
                number.slug = SlugHelper.GenerateSlug2(number.slug);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Update(NumberModel upNumber)
        {
            _context.Update(upNumber);
            await _context.SaveChangesAsync();
        }
    }
}
