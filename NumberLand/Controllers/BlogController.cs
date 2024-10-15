using AutoMapper;
using Markdig;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository;
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
        [HttpGet("SearchByCategory/{Catid}")]
        public async Task<IActionResult> GetByCategory(int Catid)
        {
            if (Catid == null || Catid == 0)
            {
                return BadRequest();
            }
            var get = _mapper.Map<List<BlogDTO>>(_unitOfWork.blog.GetAll(includeProp: "author, blogCategories.category")
        .Where(b => b.blogCategories != null && b.blogCategories.Any(bc => bc.categoryId == Catid))
        .ToList());
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


        [HttpGet("Category")]
        public async Task<IActionResult> CatGetAll()
        {
            var getall = _mapper.Map<List<BlogCategoryDTO>>(_unitOfWork.blogCategory.GetAll());
            return Ok(getall);
        }
        [HttpGet("Category/{id}")]
        public async Task<IActionResult> CatGet(int id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            var get = _mapper.Map<BlogCategoryDTO>(_unitOfWork.blogCategory.Get(o => o.id == id));
            return Ok(get);
        }

        [HttpPost("Category")]
        public async Task<IActionResult> CatCreate(BlogCategoryDTO blogCategory)
        {
            if (blogCategory == null || blogCategory.id != 0)
            {
                return BadRequest();
            }
            var mappedCat = _mapper.Map<BlogCategoryModel>(blogCategory);
            _unitOfWork.blogCategory.Add(mappedCat);
            _unitOfWork.Save();
            return Ok(blogCategory);

        }

        [HttpPut("Category/{id}")]
        public async Task<IActionResult> CatEdit(int id, BlogCategoryDTO blogCategory)
        {
            if (blogCategory == null || blogCategory.id == 0)
            {
                return BadRequest();
            }
            var mappedCat = _mapper.Map<BlogCategoryModel>(blogCategory);
            _unitOfWork.blogCategory.Update(mappedCat);
            return Ok(blogCategory);
        }

        [HttpPatch("Category/{id}")]
        public async Task<IActionResult> CatPatch(int id, [FromBody] JsonPatchDocument<BlogCategoryModel> patchDoc)
        {
            _unitOfWork.blogCategory.Patch(id, patchDoc);
            return Ok(patchDoc);
        }

        [HttpDelete("Category/{id}")]
        public async Task<IActionResult> CatRemove(int id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            var get = _unitOfWork.blogCategory.Get(o => o.id == id);
            _unitOfWork.blogCategory.Delete(get);
            _unitOfWork.Save();
            return Ok(get);
        }

        [HttpDelete("Category")]
        public async Task<IActionResult> CatRemoveRange([FromBody] List<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return BadRequest();
            }
            var get = _unitOfWork.blogCategory.GetAll().Where(p => ids.Contains(p.id)).ToList();
            _unitOfWork.blogCategory.DeleteRange(get);
            _unitOfWork.Save();
            return Ok(get);
        }
    }
}
