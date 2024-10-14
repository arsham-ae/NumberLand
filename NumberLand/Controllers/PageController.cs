using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Numbers;
using NumberLand.Models.Pages;

namespace NumberLand.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PageController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public PageController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var getall = _unitOfWork.page.GetAll(includeProp: "category");
            return Ok(getall);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            var get = _unitOfWork.page.Get(o => o.id == id, includeProp: "category");
            return Ok(get);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PageeModel page)
        {
            if (page == null || page.id != 0)
            {
                return BadRequest();
            }
            _unitOfWork.page.Add(page);
            _unitOfWork.Save();
            return Ok(page);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, PageeModel page)
        {
            if (page == null || page.id == 0)
            {
                return BadRequest();
            }
            _unitOfWork.page.Update(page);
            return Ok(page);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<PageeModel> patchDoc)
        {
            _unitOfWork.page.Patch(id, patchDoc);
            return Ok(patchDoc);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            var get = _unitOfWork.page.Get(o => o.id == id);
            _unitOfWork.page.Delete(get);
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
            var get = _unitOfWork.page.GetAll().Where(p => ids.Contains(p.id)).ToList();
            _unitOfWork.page.DeleteRange(get);
            _unitOfWork.Save();
            return Ok(get);
        }
    }
}
