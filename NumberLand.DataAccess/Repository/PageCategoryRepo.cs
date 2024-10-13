using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NumberLand.DataAccess.Data;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Numbers;
using NumberLand.Models.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberLand.DataAccess.Repository
{
    public class PageCategoryRepo : Repository<PageCategoryModel>, IPageCategoryRepo
    {
        private myDbContext _context;
        public PageCategoryRepo(myDbContext context) : base(context)
        {
            _context = context;
        }

        public void Patch(int id, [FromBody] JsonPatchDocument<PageCategoryModel> patchDoc)
        {
            var pageCategory = _context.PageCategory.FirstOrDefault(p => p.id == id);
            if (pageCategory != null && patchDoc != null)
            {
                patchDoc.ApplyTo(pageCategory);
                _context.SaveChanges();
            }
        }

        public void Update(PageCategoryModel pageCategory)
        {
            _context.Update(pageCategory);
            _context.SaveChanges();
        }
    }
}
