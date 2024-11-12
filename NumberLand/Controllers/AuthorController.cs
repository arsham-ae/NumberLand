using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using NumberLand.Command.Author.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.Models.Blogs;
using NumberLand.Query.Author.Query;

namespace NumberLand.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllAuthorsQuery();
            var result = await _mediator.Send(query);
            if (result.IsNullOrEmpty())
            {
                return NotFound("There isn't Any Authors!");
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
            var query = new GetAuthorByIdQuery(id);
            var result = await _mediator.Send(query);
            if (result == null)
            {
                return NotFound($"Author with id {id} Not Found!");
            }
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromForm] CreateAuthorDTO author, [FromForm] IFormFile file)
        {
            if (author == null || author.authorId != 0)
            {
                return BadRequest("Invalid Data!");
            }
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file was uploaded.");
            }
            var command = new CreateAuthorCommand(author, file);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Patch(int id, [FromForm] string jsonPatch, IFormFile? file)
        {
            if (string.IsNullOrWhiteSpace(jsonPatch))
            {
                return BadRequest("Patch document cannot be null or empty.");
            }
            JsonPatchDocument<AuthorModel> patchDoc;
            try
            {
                patchDoc = JsonConvert.DeserializeObject<JsonPatchDocument<AuthorModel>>(jsonPatch);
                if (patchDoc == null)
                    return BadRequest("Invalid patch document.");
            }
            catch (JsonException)
            {
                return BadRequest("Error parsing patch document.");
            }
            var command = new UpdateAuthorCommand(id, patchDoc, file);
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPatch("UpdateImage/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ImagePatch(int id, IFormFile file)
        {
            if (id == null | id == 0)
            {
                return BadRequest("Invalid Id!");
            }
            if (file == null || file.Length == 0)
            {
                return BadRequest("Image file cannot be null or empty.");
            }
            var command = new UpdateImageAuthorCommand(id, file);
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
            var command = new RemoveAuthorCommand(id);
            var result = await _mediator.Send(command);
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
            var command = new RemoveRangeAuthorCommand(ids);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
