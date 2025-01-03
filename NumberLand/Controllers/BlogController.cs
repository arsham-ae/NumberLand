using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using NumberLand.Command.Blog.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.Models.Blogs;
using NumberLand.Query.Blog.Query;
using System.Reflection.Metadata;

namespace NumberLand.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BlogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            if (id == null || id == 0)
            {
                return BadRequest("Invalid Id!");
            }
            var query = new GetBlogByIdQuery(id);
            var result = await _mediator.Send(query);
            if (result == null)
            {
                return NotFound($"Blog With Id {id} Not Found");
            }
            return Ok(result);
        }
        [HttpGet("SearchBySlug/{slug}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBySlug(string slug)
        {
            if (String.IsNullOrEmpty(slug))
            {
                return BadRequest("Invalid Slug!");
            }
            var query = new GetBlogBySlugQuery(slug);
            var result = await _mediator.Send(query);
            if (result == null)
            {
                return NotFound($"Blog With Slug \"{slug}\" Not Found");
            }
            return Ok(result);
        }

        [HttpGet("SearchByAuthor/{authorSlug}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByAuthor(string authorSlug)
        {
            if (String.IsNullOrEmpty(authorSlug))
            {
                return BadRequest("Invalid Slug!");
            }
            var result = await _mediator.Send(new GetBlogByAuthorSlugQuery(authorSlug));
            if (result.IsNullOrEmpty())
            {
                return NotFound($"No Blog Found With Author Slug \"{authorSlug}\" ");
            }
            return Ok(result);
        }

        [HttpGet("SearchByCategory/{catSlug}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByCategory(string catSlug)
        {
            if (String.IsNullOrEmpty(catSlug))
            {
                return BadRequest("Invalid Slug!");
            }
            var result = await _mediator.Send(new GetBlogByCatSlugQuery(catSlug));
            if (result.IsNullOrEmpty())
            {
                return NotFound($"No Blog Found With Category Slug \"{catSlug}\" ");
            }
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromForm] CreateBlogDTO blog, IFormFile file)
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Patch(int id, [FromForm] string? jsonPatch, IFormFile? file)
        {
            if (string.IsNullOrEmpty(jsonPatch) && file == null)
            {
                return BadRequest("Patch Document or File must be imported.");
            }
            if (!string.IsNullOrEmpty(jsonPatch))
            {
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
            if (file != null)
            {
                var command = new UpdateBlogCommand(id, null, file);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            return BadRequest("Unexpected error occurred.");
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveRange([FromBody] List<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return BadRequest("Invalid Ids!");
            }
            var result = await _mediator.Send(new RemoveRangeBlogCommand(ids));
            return Ok(result);
        }



        [HttpGet("Category")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CatGetAll()
        {
            var result = await _mediator.Send(new GetAllBlogsCategoryQuery());
            if (result.IsNullOrEmpty())
            {
                return NotFound("There isn't Any Category");
            }
            return Ok(result);
        }
        [HttpGet("Category/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CatGet(int id)
        {
            if (id == null || id == 0)
            {
                return BadRequest("invalid Id!");
            }
            var result = await _mediator.Send(new GetBlogCategoryByIdQuery(id));
            if (result == null)
            {
                return NotFound($"Category With Id {id} Not Found");
            }

            return Ok(result);
        }
        [HttpGet("Category/SearchBySlug/{slug}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCatBySlug(string slug)
        {
            if (String.IsNullOrEmpty(slug))
            {
                return BadRequest("Invalid Slug!");
            }
            var query = new GetBlogCategoryBySlugQuery(slug);
            var result = await _mediator.Send(query);
            if (result == null)
            {
                return NotFound($"BlogCategory With Slug \"{slug}\" Not Found");
            }
            return Ok(result);
        }
        [HttpPost("Category")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CatCreate([FromForm] CreateBlogCategoryDTO blogCategory, IFormFile file)
        {
            if (blogCategory == null || blogCategory.blogCategoryId != 0)
            {
                return BadRequest();
            }
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file was uploaded.");
            }
            var command = new CreateBlogCategoryCommand(blogCategory, file);
            var result = await _mediator.Send(command);
            return Ok(result);

        }

        [HttpPatch("Category/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CatPatch(int id, [FromForm] string? jsonPatch, IFormFile? file)
        {
            if (string.IsNullOrEmpty(jsonPatch) && file == null)
            {
                return BadRequest("Patch Document or File must be imported.");
            }
            if (!string.IsNullOrEmpty(jsonPatch))
            {
                JsonPatchDocument<BlogCategoryModel> patchDoc;
                try
                {
                    patchDoc = JsonConvert.DeserializeObject<JsonPatchDocument<BlogCategoryModel>>(jsonPatch);
                    if (patchDoc == null)
                    {
                        return BadRequest("Invalid patch document.");
                    }
                }
                catch (JsonException)
                {
                    return BadRequest("Error parsing patch document.");
                }
                var command = new UpdateBlogCategoryCommand(id, patchDoc, file);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            if (file != null)
            {
                var command = new UpdateBlogCategoryCommand(id, null, file);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            return BadRequest("Unexpected error occurred.");
        }

        [HttpDelete("Category/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CatRemove(int id)
        {
            if (id == null || id == 0)
            {
                return BadRequest("Invalid Id!");
            }
            var result = await _mediator.Send(new RemoveBlogCategoryCommand(id));
            return Ok(result);
        }

        [HttpDelete("Category")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CatRemoveRange([FromBody] List<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return BadRequest("Invalid Id!");
            }
            var result = await _mediator.Send(new RemoveRangeBlogCategoryCommand(ids));
            return Ok(result);
        }
    }
}
