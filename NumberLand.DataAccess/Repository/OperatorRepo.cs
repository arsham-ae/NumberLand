using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NumberLand.DataAccess.Data;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Numbers;

namespace NumberLand.DataAccess.Repository
{
    public class OperatorRepo : Repository<OperatorModel>, IOperatorRepo
    {
        private myDbContext _context;
        public OperatorRepo(myDbContext context) : base(context)
        {
            _context = context;
        }
        public void Patch(int id, [FromBody] JsonPatchDocument<OperatorModel> patchDoc)
        {
            var nOperator = _context.Operator.FirstOrDefault(p => p.id == id);
            if (nOperator != null && patchDoc != null)
            {
                patchDoc.ApplyTo(nOperator);
                _context.SaveChanges();
            }
        }

        public void Update(OperatorModel upOperator)
        {
            _context.Update(upOperator);
            _context.SaveChanges();
        }
    }
}
