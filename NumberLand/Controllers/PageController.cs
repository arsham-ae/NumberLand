using Markdig;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Numbers;
using NumberLand.Models.Pages;
using NumberLand.Utility;
using System.Reflection.Metadata;

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
            var pipeLine = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            page.content = Markdown.ToHtml(page.content, pipeLine);
            page.slug = SlugHelper.GenerateSlug(page.title);
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
            var pipeLine = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            page.content = Markdown.ToHtml(page.content, pipeLine);
            page.slug = SlugHelper.GenerateSlug(page.title);
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



        [HttpGet("Category")]
        public async Task<IActionResult> CatGetAll()
        {
            var getall = _unitOfWork.pageCategory.GetAll(includeProp: "parentCategory");
            return Ok(getall);
        }
        [HttpGet("Category/{id}")]
        public async Task<IActionResult> CatGet(int id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            var get = _unitOfWork.pageCategory.Get(o => o.id == id, includeProp: "parentCategory");
            return Ok(get);
        }

        [HttpPost("Category")]
        public async Task<IActionResult> CatCreate(PageCategoryModel pageCategory)
        {
            if (pageCategory == null || pageCategory.id != 0)
            {
                return BadRequest();
            }
            _unitOfWork.pageCategory.Add(pageCategory);
            _unitOfWork.Save();
            return Ok(pageCategory);

        }

        [HttpPut("Category/{id}")]
        public async Task<IActionResult> CatEdit(int id, PageCategoryModel pageCategory)
        {
            if (pageCategory == null || pageCategory.id == 0)
            {
                return BadRequest();
            }
            _unitOfWork.pageCategory.Update(pageCategory);
            return Ok(pageCategory);
        }

        [HttpPatch("Category/{id}")]
        public async Task<IActionResult> CatPatch(int id, [FromBody] JsonPatchDocument<PageCategoryModel> patchDoc)
        {
            _unitOfWork.pageCategory.Patch(id, patchDoc);
            return Ok(patchDoc);
        }

        [HttpDelete("Category/{id}")]
        public async Task<IActionResult> CatRemove(int id)
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

        [HttpDelete("Category")]
        public async Task<IActionResult> CatRemoveRange([FromBody] List<int> ids)
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
