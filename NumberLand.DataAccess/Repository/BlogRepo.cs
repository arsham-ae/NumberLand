using Markdig;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NumberLand.DataAccess.Data;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Blogs;

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

        public async void Update(BlogModel blog)
        {
            var pipeLine = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            var existingBlog = await _context.Blog
            .Include(b => b.blogCategories)
            .FirstOrDefaultAsync(b => b.id == blog.id);

            if (existingBlog == null)
            {
                throw new KeyNotFoundException("Not Found!");
            }

            existingBlog.title = blog.title;
            blog.content = Markdown.ToHtml(blog.content, pipeLine);
            existingBlog.content = blog.content;
            existingBlog.featuredImagePath = blog.featuredImagePath;
            existingBlog.updateAt = DateTime.Now;
            existingBlog.authorId = blog.authorId;
            existingBlog.isPublished = blog.isPublished;

            existingBlog.blogCategories.Clear();

            if (blog.blogCategories != null && blog.blogCategories.Any())
            {
                //Add the updated BlogCategories
                foreach (var category in blog.blogCategories)
                {
                    existingBlog.blogCategories.Add(new BlogCategoryJoinModel
                    {
                        blogId = blog.id,
                        categoryId = category.categoryId
                    });
                }
            }

            _context.Update(existingBlog);
            await _context.SaveChangesAsync();
        }
    }
}
