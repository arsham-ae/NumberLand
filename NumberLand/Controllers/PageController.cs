using AutoMapper;
using Markdig;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NumberLand.DataAccess.DTOs;
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
        private readonly IMapper    _mapper;
        public PageController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var getall = _mapper.Map<List<PageDTO>>(_unitOfWork.page.GetAll(includeProp: "category"));
            return Ok(getall);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            var get = _mapper.Map<PageDTO>(_unitOfWork.page.Get(o => o.id == id, includeProp: "category"));
            return Ok(get);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePageDTO page)
        {
            if (page == null || page.id != 0)
            {
                return BadRequest();
            }
            var mappedPage = _mapper.Map<PageeModel>(page);
            var pipeLine = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            mappedPage.content = Markdown.ToHtml(mappedPage.content, pipeLine);
            mappedPage.slug = SlugHelper.GenerateSlug(mappedPage.title);
            _unitOfWork.page.Add(mappedPage);
            _unitOfWork.Save();
            return Ok(page);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, CreatePageDTO page)
        {
            if (page == null || page.id == 0)
            {
                return BadRequest();
            }
            var mappedPage = _mapper.Map<PageeModel>(page);
            var pipeLine = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            mappedPage.content = Markdown.ToHtml(mappedPage.content, pipeLine);
            mappedPage.slug = SlugHelper.GenerateSlug(mappedPage.title);
            _unitOfWork.page.Update(mappedPage);
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
            var getall = _mapper.Map<List<PageCategoryDTO>>(_unitOfWork.pageCategory.GetAll(includeProp: "parentCategory"));
            return Ok(getall);
        }
        [HttpGet("Category/{id}")]
        public async Task<IActionResult> CatGet(int id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            var get = _mapper.Map<PageCategoryDTO>(_unitOfWork.pageCategory.Get(o => o.id == id, includeProp: "parentCategory"));
            return Ok(get);
        }

        [HttpPost("Category")]
        public async Task<IActionResult> CatCreate(CreatePageCategoryDTO pageCategory)
        {
            if (pageCategory == null || pageCategory.id != 0)
            {
                return BadRequest();
            }
            var mappedCat = _mapper.Map<PageCategoryModel>(pageCategory);
            _unitOfWork.pageCategory.Add(mappedCat);
            _unitOfWork.Save();
            return Ok(pageCategory);

        }

        [HttpPut("Category/{id}")]
        public async Task<IActionResult> CatEdit(int id, CreatePageCategoryDTO pageCategory)
        {
            if (pageCategory == null || pageCategory.id == 0)
            {
                return BadRequest();
            }
            var mappedCat = _mapper.Map<PageCategoryModel>(pageCategory);
            _unitOfWork.pageCategory.Update(mappedCat);
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
