using AutoMapper;
using Markdig;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Blogs;
using NumberLand.Models.Pages;

namespace NumberLand.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BlogController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var getall = _mapper.Map<List<BlogDTO>>(_unitOfWork.blog.GetAll(includeProp: "author, blogCategories.category"));
            return Ok(getall);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            var get = _mapper.Map<BlogDTO>(_unitOfWork.blog.Get(o => o.id == id, includeProp: "author, blogCategories.category"));
            return Ok(get);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBlogDTO blog)
        {
            if (blog == null || blog.id != 0)
            {
                return BadRequest();
            }
            var pipeLine = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            blog.content = Markdown.ToHtml(blog.content, pipeLine);
            var mappedBlog = _mapper.Map<BlogModel>(blog);
            mappedBlog.blogCategories = new List<BlogCategoryJoinModel>();
            foreach (var categoryId in blog.blogCategories)
            {
                mappedBlog.blogCategories.Add(new BlogCategoryJoinModel
                {
                    categoryId = categoryId
                });
            }

            _unitOfWork.blog.Add(mappedBlog);
            _unitOfWork.Save();
            return Ok(blog);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, CreateBlogDTO blog)
        {
            if (blog == null || blog.id == 0)
            {
                return BadRequest();
            }
            var pipeLine = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            blog.content = Markdown.ToHtml(blog.content, pipeLine);
            blog.blogCategories.Clear();
            var mappedBlog = _mapper.Map<BlogModel>(blog);
            mappedBlog.blogCategories = new List<BlogCategoryJoinModel>();
            foreach (var categoryId in blog.blogCategories)
            {
                mappedBlog.blogCategories.Add(new BlogCategoryJoinModel
                {
                    categoryId = categoryId
                });
            }
            _unitOfWork.blog.Update(mappedBlog);
            return Ok(blog);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<BlogModel> patchDoc)
        {
            _unitOfWork.blog.Patch(id, patchDoc);
            return Ok(patchDoc);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            var get = _unitOfWork.blog.Get(o => o.id == id);
            _unitOfWork.blog.Delete(get);
            _unitOfWork.Save();
            return Ok(get);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveRange([FromBody] List<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return BadRequest();
            }
            var get = _unitOfWork.blog.GetAll().Where(p => ids.Contains(p.id)).ToList();
            _unitOfWork.blog.DeleteRange(get);
            _unitOfWork.Save();
            return Ok(get);
        }
    }
}
