using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using NumberLand.Command.Author.Command;
using NumberLand.Command.Blog.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.Models.Blogs;
using NumberLand.Query.Blog.Query;

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
        [HttpGet("SearchByCategory/{Catid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByCategory(int Catid)
        {
            if (Catid == null || Catid == 0)
            {
                return BadRequest("Invalid Cat Id");
            }
            var result = await _mediator.Send(new GetByCatIdQuery(Catid));
            if (result.IsNullOrEmpty())
            {
                return NotFound($"No Blog Found With Category Id {Catid}");
            }
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        [HttpPatch("UpdateImage/{id}")]
        public async Task<IActionResult> UpdateImage(int id, IFormFile file)
        {
            if (id == null || id == 0)
            {
                return BadRequest("Invalid Id!");
            }
            if (file == null || file.Length == 0)
            {
                return BadRequest("Image file cannot be null or empty.");
            }
            var command = new UpdateBlogImageCommand(id, file);
            var result = await _mediator.Send(command);
            return Ok(result);
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

        [HttpPost("Category")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CatCreate(BlogCategoryDTO blogCategory)
        {
            if (blogCategory == null || blogCategory.blogCategoryId != 0)
            {
                return BadRequest();
            }
            var command = new CreateBlogCategoryCommand(blogCategory);
            var result = await _mediator.Send(command);
            return Ok(result);

        }

        [HttpPatch("Category/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CatPatch(int id, [FromBody] JsonPatchDocument<BlogCategoryModel> patchDoc)
        {
            if (id == 0 || patchDoc == null)
            {
                return BadRequest("Invalid Data!");
            }
            var command = new UpdateBlogCategoryCommand(id, patchDoc);
            var result = await _mediator.Send(command);
            return Ok(patchDoc);
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
