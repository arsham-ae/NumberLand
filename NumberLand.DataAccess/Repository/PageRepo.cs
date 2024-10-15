using Markdig;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NumberLand.DataAccess.Data;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberLand.DataAccess.Repository
{
    public class PageRepo : Repository<PageeModel>, IPageRepo
    {
        private myDbContext _context;
        public PageRepo(myDbContext context) : base(context)
        {
            _context = context;
        }
        public void Patch(int id, [FromBody] JsonPatchDocument<PageeModel> patchDoc)
        {
            var pipeLine = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            var page = _context.Page.FirstOrDefault(p => p.id == id);
            if (page != null && patchDoc != null)
            {
                patchDoc.ApplyTo(page);
                page.content = Markdown.ToHtml(page.content, pipeLine);
                _context.SaveChanges();
            }
        }

        public void Update(PageeModel page)
        {
            _context.Update(page);
            _context.SaveChanges();
        }
    }
}
