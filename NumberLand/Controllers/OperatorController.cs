using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NumberLand.Command.Operator.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.Models.Numbers;
using NumberLand.Query.Blog.Query;
using NumberLand.Query.Operator.Query;

namespace NumberLand.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperatorController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OperatorController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllOperatorsQuery());
            if (result.IsNullOrEmpty())
            {
                return NotFound("There isn't Any Operators.");
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
            var result = await _mediator.Send(new GetOperatorByIdQuery(id));
            if (result == null)
            {
                return NotFound($"Operator With Id {id} Not Found.");
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
            var query = new GetOperatorBySlugQuery(slug);
            var result = await _mediator.Send(query);
            if (result == null)
            {
                return NotFound($"Operator With Slug \"{slug}\" Not Found");
            }
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateOperatorDTO operatorModel)
        {
            if (operatorModel == null || operatorModel.id != 0)
            {
                return BadRequest("invalid Data!");
            }
            var command = new CreateOperatorCommand(operatorModel);
            var result = await _mediator.Send(command);
            return Ok(result);

        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<OperatorModel> patchDoc)
        {
            if (id == 0 || patchDoc == null)
            {
                return BadRequest("Invalid Data!");
            }
            var command = new UpdateOperatorCommand(id, patchDoc);
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
            var result = await _mediator.Send(new RemoveOperatorCommand(id));
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
            var result = await _mediator.Send(new RemoveRangeOperatorCommand(ids));
            return Ok(result);
        }
    }
}
