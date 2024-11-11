using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NumberLand.Command.Operator.Command;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Numbers;
using NumberLand.Query.Operator.Query;
using NumberLand.Utility;

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
        public async Task<IActionResult> Get(int id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            var result = await _mediator.Send(new GetOperatorByIdQuery(id));
            if (result == null)
            {
                return NotFound($"Operator With Id {id} Not Found.");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OperatorModel operatorModel)
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
