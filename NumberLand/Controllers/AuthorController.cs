using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Blogs;
using NumberLand.Models.Pages;
using NumberLand.Utility;

namespace NumberLand.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public AuthorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var getall = _unitOfWork.author.GetAll();
            return Ok(getall);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            var get = _unitOfWork.author.Get(o => o.id == id);
            return Ok(get);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AuthorModel author)
        {
            if (author == null || author.id != 0)
            {
                return BadRequest();
            }
            author.slug = SlugHelper.GenerateSlug(author.name);
            _unitOfWork.author.Add(author);
            _unitOfWork.Save();
            return Ok(author);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, AuthorModel author)
        {
            if (author == null || author.id == 0)
            {
                return BadRequest();
            }
            author.slug = SlugHelper.GenerateSlug(author.name);
            _unitOfWork.author.Update(author);
            return Ok(author);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<AuthorModel> patchDoc)
        {
            _unitOfWork.author.Patch(id, patchDoc);
            return Ok(patchDoc);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            var get = _unitOfWork.author.Get(o => o.id == id);
            _unitOfWork.author.Delete(get);
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
            var get = _unitOfWork.author.GetAll().Where(p => ids.Contains(p.id)).ToList();
            _unitOfWork.author.DeleteRange(get);
            _unitOfWork.Save();
            return Ok(get);
        }
    }
}
