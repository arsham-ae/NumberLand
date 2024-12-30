using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NumberLand.DataAccess.Data;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Numbers;
using NumberLand.Utility;

namespace NumberLand.DataAccess.Repository
{
    public class OperatorRepo : Repository<OperatorModel>, IOperatorRepo
    {
        private myDbContext _context;
        public OperatorRepo(myDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task Patch(int id, [FromBody] JsonPatchDocument<OperatorModel> patchDoc)
        {
            var nOperator = await _context.Operator.FirstOrDefaultAsync(p => p.id == id);
            if (nOperator != null && patchDoc != null)
            {
                patchDoc.ApplyTo(nOperator);
                nOperator.slug = SlugHelper.GenerateSlug2(nOperator.operatorCode);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Update(OperatorModel upOperator)
        {
            _context.Update(upOperator);
            await _context.SaveChangesAsync();
        }
    }
}
