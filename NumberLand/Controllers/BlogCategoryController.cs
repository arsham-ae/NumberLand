using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Blogs;

namespace NumberLand.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogCategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public BlogCategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var getall = _unitOfWork.blogCategory.GetAll();
            return Ok(getall);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            var get = _unitOfWork.blogCategory.Get(o => o.id == id);
            return Ok(get);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BlogCategoryModel blogCategory)
        {
            if (blogCategory == null || blogCategory.id != 0)
            {
                return BadRequest();
            }
            _unitOfWork.blogCategory.Add(blogCategory);
            _unitOfWork.Save();
            return Ok(blogCategory);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, BlogCategoryModel blogCategory)
        {
            if (blogCategory == null || blogCategory.id == 0)
            {
                return BadRequest();
            }
            _unitOfWork.blogCategory.Update(blogCategory);
            return Ok(blogCategory);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<BlogCategoryModel> patchDoc)
        {
            _unitOfWork.blogCategory.Patch(id, patchDoc);
            return Ok(patchDoc);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
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

        [HttpDelete]
        public async Task<IActionResult> RemoveRange([FromBody] List<int> ids)
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
