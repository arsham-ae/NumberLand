using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NumberLand.DataAccess.Data;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Numbers;

namespace NumberLand.DataAccess.Repository
{
    public class CountryRepo : Repository<CountryModel>, ICountryRepo
    {
        private myDbContext _context;
        public CountryRepo(myDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task Patch(int id, [FromBody] JsonPatchDocument<CountryModel> patchDoc)
        {
            var country = await _context.Country.FirstOrDefaultAsync(p => p.id == id);
            if (country != null && patchDoc != null)
            {
                patchDoc.ApplyTo(country);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Update(CountryModel country)
        {
            _context.Update(country);
            await _context.SaveChangesAsync();
        }
    }
}
