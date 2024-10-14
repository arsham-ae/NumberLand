using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Pages;

namespace NumberLand.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PageCategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public PageCategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var getall = _unitOfWork.pageCategory.GetAll(includeProp: "parentCategory");
            return Ok(getall);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            var get = _unitOfWork.pageCategory.Get(o => o.id == id, includeProp: "parentCategory");
            return Ok(get);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PageCategoryModel pageCategory)
        {
            if (pageCategory == null || pageCategory.id != 0)
            {
                return BadRequest();
            }
            _unitOfWork.pageCategory.Add(pageCategory);
            _unitOfWork.Save();
            return Ok(pageCategory);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, PageCategoryModel pageCategory)
        {
            if (pageCategory == null || pageCategory.id == 0)
            {
                return BadRequest();
            }
            _unitOfWork.pageCategory.Update(pageCategory);
            return Ok(pageCategory);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<PageCategoryModel> patchDoc)
        {
            _unitOfWork.pageCategory.Patch(id, patchDoc);
            return Ok(patchDoc);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            var get = _unitOfWork.pageCategory.Get(o => o.id == id);
            _unitOfWork.pageCategory.Delete(get);
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
            var get = _unitOfWork.pageCategory.GetAll().Where(p => ids.Contains(p.id)).ToList();
            _unitOfWork.pageCategory.DeleteRange(get);
            _unitOfWork.Save();
            return Ok(get);
        }
    }
}
