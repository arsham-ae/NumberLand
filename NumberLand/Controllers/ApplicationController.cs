using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using NumberLand.Command.Application.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.Models.Numbers;
using NumberLand.Query.Application.Query;

namespace NumberLand.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ApplicationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllApplicationsQuery());
            if (result.IsNullOrEmpty())
            {
                return NotFound("There isn't Any Applications.");
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
            var result = await _mediator.Send(new GetApplicationByIdQuery(id));
            if (result == null)
            {
                return NotFound($"Application With Id {id} Not Found.");
            }
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromForm] CreateApplicationDTO application, [FromForm] IFormFile file)
        {
            if (application == null || application.appId != 0)
            {
                return BadRequest("invalid Data!");
            }
            var command = new CreateApplicationCommand(application, file);
            var result = await _mediator.Send(command);
            return Ok(result);

        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Patch(int id, [FromForm] string jsonPatch, [FromForm] IFormFile? file)
        {
            if (string.IsNullOrEmpty(jsonPatch))
            {
                return BadRequest("Patch document cannot be null or empty.");
            }
            JsonPatchDocument<ApplicationModel> patchDoc;
            try
            {
                patchDoc = JsonConvert.DeserializeObject<JsonPatchDocument<ApplicationModel>>(jsonPatch);
                if (patchDoc == null)
                {
                    return BadRequest("Invalid patch document.");
                }
            }
            catch (JsonException)
            {
                return BadRequest("Error parsing patch document.");
            }
            var command = new UpdateApplicationCommand(id, patchDoc, file);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPatch("UpdateImage/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdadteImage(int id, [FromForm] IFormFile file)
        {
            if (id == null || id == 0)
            {
                return BadRequest("Invalid Id!");
            }
            if (file == null || file.Length == 0)
            {
                return BadRequest("Image file cannot be null or empty.");
            }
            var command = new UpdateApplicationImageCommand(id, file);
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
            var result = await _mediator.Send(new RemoveApplicationCommand(id));
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
            var result = await _mediator.Send(new RemoveRangeApplicationCommand(ids));
            return Ok(result);
        }
    }
}
