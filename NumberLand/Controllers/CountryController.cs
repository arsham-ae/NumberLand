using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using NumberLand.Command.Author.Command;
using NumberLand.Command.Country.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.Models.Blogs;
using NumberLand.Models.Numbers;
using NumberLand.Query.Blog.Query;
using NumberLand.Query.Country.Query;

namespace NumberLand.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CountryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllCountriesQuery());
            if (result.IsNullOrEmpty())
            {
                return NotFound("There isn't Any Countries.");
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
            var result = await _mediator.Send(new GetCountryByIdQuery(id));
            if (result == null)
            {
                return NotFound($"Country With Id {id} Not Found.");
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
            var query = new GetCountryBySlugQuery(slug);
            var result = await _mediator.Send(query);
            if (result == null)
            {
                return NotFound($"Country With Slug \"{slug}\" Not Found");
            }
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromForm] CreateCountryDTO country, IFormFile file)
        {
            if (country == null || country.countryId != 0)
            {
                return BadRequest("invalid Data!");
            }
            var command = new CreateCountryCommand(country, file);
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
                JsonPatchDocument<CountryModel> patchDoc;
                try
                {
                    patchDoc = JsonConvert.DeserializeObject<JsonPatchDocument<CountryModel>>(jsonPatch);
                    if (patchDoc == null)
                    {
                        return BadRequest("Invalid patch document.");
                    }
                }
                catch (JsonException)
                {
                    return BadRequest("Error parsing patch document.");
                }
                var command = new UpdateCountryCommand(id, patchDoc, file);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            if (file != null)
            {
                var command = new UpdateCountryCommand(id, null, file);
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
            var result = await _mediator.Send(new RemoveCountryCommand(id));
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
            var result = await _mediator.Send(new RemoveRangeCountryCommand(ids));
            return Ok(result);
        }
    }
}
