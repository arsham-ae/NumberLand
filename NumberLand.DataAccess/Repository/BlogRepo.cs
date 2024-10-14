using Markdig;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NumberLand.DataAccess.Data;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Blogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberLand.DataAccess.Repository
{
    public class BlogRepo : Repository<BlogModel>, IBlogRepo
    {
        private myDbContext _context;
        public BlogRepo(myDbContext context) : base(context)
        {
            _context = context;
        }

        public void Patch(int id, [FromBody] JsonPatchDocument<BlogModel> patchDoc)
        {
            var pipeLine = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            var blog = _context.Blog.FirstOrDefault(p => p.id == id);
            if (blog != null || patchDoc != null)
            {
                patchDoc.ApplyTo(blog);
                blog.content = Markdown.ToHtml(blog.content, pipeLine);
                _context.SaveChanges();
            }
        }

        public void Update(BlogModel blog)
        {
            _context.Update(blog);
            _context.SaveChanges();
        }
    }
}
