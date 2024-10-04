using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NumberLand.DataAccess.Data;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NumberLand.DataAccess.Repository
{
    public class NumberRepo : Repository<NumberModel>, INumberRepo
    {
        private myDbContext _context;
        public NumberRepo(myDbContext context) : base(context)
        {
            _context = context;
        }
        public void Patch(int id, [FromBody] JsonPatchDocument<NumberModel> patchDoc)
        {
            var number = _context.Numbers.FirstOrDefault(p => p.id == id);
            if (number != null && patchDoc != null)
            {
                patchDoc.ApplyTo(number);
                _context.SaveChanges();
            }
        }

        public void Update(NumberModel upNumber)
        {
            _context.Update(upNumber);
            _context.SaveChanges();
        }
    }
}
