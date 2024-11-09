using AutoMapper;
using Markdig;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using NumberLand.Command.Blog.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Blogs;
using NumberLand.Query.Blog.Query;
using NumberLand.Utility;

namespace NumberLand.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<BlogController> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly IMediator _mediator;
        public BlogController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<BlogController> logger, IWebHostEnvironment environment, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _environment = environment;
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllBlogsQuery();
            var result = await _mediator.Send(query);
            if (result.IsNullOrEmpty())
            {
                return NotFound("There isn't Any Blogs");
            }
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            var query = new GetBlogByIdQuery(id);
            var result = await _mediator.Send(query);
            if (result == null)
            {
                return NotFound($"Blog With Id {id} Not Found");
            }
            return Ok(result);
        }
        [HttpGet("SearchByCategory/{Catid}")]
        public async Task<IActionResult> GetByCategory(int Catid)
        {
            if (Catid == null || Catid == 0)
            {
                return BadRequest();
            }
            var get = _mapper.Map<List<BlogDTO>>((await _unitOfWork.blog.GetAll(includeProp: "author, blogCategories.category"))
        .Where(b => b.blogCategories != null && b.blogCategories.Any(bc => bc.categoryId == Catid))
        .ToList());
            return Ok(get);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateBlogDTO blog, [FromForm] IFormFile file)
        {
            if (blog == null || blog.blogId != 0)
            {
                return BadRequest("Invalid Blog Data!");
            }
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file was uploaded.");
            }
            var command = new CreateBlogCommand(blog, file);
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] string jsonPatch, IFormFile? file)
        {
            if (!string.IsNullOrEmpty(jsonPatch))
            {
                return BadRequest("Patch document cannot be null or empty.");
            }
            JsonPatchDocument<BlogModel> patchDoc;
            try
            {
                patchDoc = JsonConvert.DeserializeObject<JsonPatchDocument<BlogModel>>(jsonPatch);
                if (patchDoc == null)
                {
                    return BadRequest("Invalid patch document.");
                }
            }
            catch (JsonException)
            {
                return BadRequest("Error parsing patch document.");
            }

            var command = new UpdateBlogCommand(id, patchDoc, file);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            if (id == null || id == 0)
            {
                return BadRequest("Invalid Id!");
            }
            var result = await _mediator.Send(new RemoveBlogCommand(id));
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveRange([FromBody] List<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return BadRequest();
            }
            var result = await _mediator.Send(new RemoveRangeBlogCommand(ids));
            return Ok(result);
        }



        [HttpGet("Category")]
        public async Task<IActionResult> CatGetAll()
        {
            var getall = _mapper.Map<List<BlogCategoryDTO>>(await _unitOfWork.blogCategory.GetAll());
            return Ok(getall);
        }
        [HttpGet("Category/{id}")]
        public async Task<IActionResult> CatGet(int id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            var get = _mapper.Map<BlogCategoryDTO>(await _unitOfWork.blogCategory.Get(o => o.id == id));
            return Ok(get);
        }

        [HttpPost("Category")]
        public async Task<IActionResult> CatCreate(BlogCategoryDTO blogCategory)
        {
            if (blogCategory == null || blogCategory.blogCategoryId != 0)
            {
                return BadRequest();
            }
            var mappedCat = _mapper.Map<BlogCategoryModel>(blogCategory);
            mappedCat.slug = SlugHelper.GenerateSlug(blogCategory.blogCategoryName);
            _unitOfWork.blogCategory.Add(mappedCat);
            await _unitOfWork.Save();
            return Ok(blogCategory);

        }

        [HttpPut("Category/{id}")]
        public async Task<IActionResult> CatEdit(int id, BlogCategoryDTO blogCategory)
        {
            if (blogCategory == null || blogCategory.blogCategoryId == 0)
            {
                return BadRequest();
            }
            var mappedCat = _mapper.Map<BlogCategoryModel>(blogCategory);
            mappedCat.slug = SlugHelper.GenerateSlug(blogCategory.blogCategoryName);
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
            var get = await _unitOfWork.blogCategory.Get(o => o.id == id);
            _unitOfWork.blogCategory.Delete(get);
            await _unitOfWork.Save();
            return Ok(get);
        }

        [HttpDelete("Category")]
        public async Task<IActionResult> CatRemoveRange([FromBody] List<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return BadRequest();
            }
            var get = (await _unitOfWork.blogCategory.GetAll()).Where(p => ids.Contains(p.id)).ToList();
            _unitOfWork.blogCategory.DeleteRange(get);
            await _unitOfWork.Save();
            return Ok(get);
        }
    }
}
