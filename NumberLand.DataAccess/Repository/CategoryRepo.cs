using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NumberLand.DataAccess.Data;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Numbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberLand.DataAccess.Repository
{
    public class CategoryRepo : Repository<CategoryModel>, ICategoryRepo
    {
        private myDbContext _context;
        public CategoryRepo(myDbContext context) : base(context)
        {
            _context = context;
        }

        public void Patch(int id, [FromBody] JsonPatchDocument<CategoryModel> patchDoc)
        {
            var category = _context.Category.FirstOrDefault(p => p.id == id);
            if (category != null && patchDoc != null)
            {
                patchDoc.ApplyTo(category);
                _context.SaveChanges();
            }
        }

        public void Update(CategoryModel category)
        {
            _context.Update(category);
            _context.SaveChanges();
        }
    }
}
