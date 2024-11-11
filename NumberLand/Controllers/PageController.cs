using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NumberLand.Command.Page.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.Models.Pages;
using NumberLand.Query.Page.Query;

namespace NumberLand.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PageController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllPagesQuery());
            if (result.IsNullOrEmpty())
            {
                return NotFound("There isn't Any Page!");
            }
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == null || id == 0)
            {
                return BadRequest("Invalid Id!");
            }
            var result = await _mediator.Send(new GetPageByIdQuery(id));
            if (result == null)
            {
                return NotFound($"Page With Id {id} Not Found.");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePageDTO page)
        {
            if (page == null || page.id != 0)
            {
                return BadRequest("Invalid Data!");
            }
            var command = new CreatePageCommand(page);
            var result = await _mediator.Send(command);
            return Ok(result);

        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<PageeModel> patchDoc)
        {
            if (id == 0 || patchDoc == null)
            {
                return BadRequest("Invalid Data!");
            }
            var command = new UpdatePageCommand(id, patchDoc);
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
            var result = await _mediator.Send(new RemovePageCommand(id));
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveRange([FromBody] List<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return BadRequest("Invalid Ids!");
            }
            var result = await _mediator.Send(new RemoveRangePageCommand(ids));
            return Ok(result);
        }



        [HttpGet("Category")]
        public async Task<IActionResult> CatGetAll()
        {
            var result = await _mediator.Send(new GetAllPagesCategoryQuery());
            if (result.IsNullOrEmpty())
            {
                return NotFound("There isn't Any PageCategory!");
            }
            return Ok(result);
        }
        [HttpGet("Category/{id}")]
        public async Task<IActionResult> CatGet(int id)
        {
            if (id == null || id == 0)
            {
                return BadRequest("Invalid Id!");
            }
            var result = await _mediator.Send(new GetPageCategoryByIdQuery(id));
            if (result == null)
            {
                return NotFound($"PageCategory With Id {id} Not Found.");
            }
            return Ok(result);
        }

        [HttpPost("Category")]
        public async Task<IActionResult> CatCreate(CreatePageCategoryDTO pageCategory)
        {
            if (pageCategory == null || pageCategory.id != 0)
            {
                return BadRequest("Invalid Data!");
            }
            var command = new CreatePageCategoryCommand(pageCategory);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPatch("Category/{id}")]
        public async Task<IActionResult> CatPatch(int id, [FromBody] JsonPatchDocument<PageCategoryModel> patchDoc)
        {
            if (id == 0 || patchDoc == null)
            {
                return BadRequest("Invalid Data!");
            }
            var command = new UpdatePageCategoryCommand(id, patchDoc);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("Category/{id}")]
        public async Task<IActionResult> CatRemove(int id)
        {
            if (id == null || id == 0)
            {
                return BadRequest("Invalid Id!");
            }
            var result = await _mediator.Send(new RemovePageCategoryCommand(id));
            return Ok(result);
        }

        [HttpDelete("Category")]
        public async Task<IActionResult> CatRemoveRange([FromBody] List<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return BadRequest("Invalid Id!");
            }
            var result = await _mediator.Send(new RemoveRangePageCategoryCommand(ids));
            return Ok(result);
        }
    }
}
