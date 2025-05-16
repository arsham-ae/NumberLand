using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using NumberLand.Command.Application.Command;
using NumberLand.Command.Author.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Helper;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Blogs;
using NumberLand.Models.Numbers;
using NumberLand.Query.Application.Query;
using NumberLand.Query.Blog.Query;

namespace NumberLand.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ITranslationService _translationService;
        private readonly IHttpContextAccessor _contextAccessor;

        public ApplicationController(IMediator mediator, ITranslationService translationService, IHttpContextAccessor contextAccessor)
        {
            _mediator = mediator;
            _translationService = translationService;
            _contextAccessor = contextAccessor;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll()
        {
            var lang = LanguageHelper.GetLanguage(_contextAccessor.HttpContext);
            var result = await _mediator.Send(new GetAllApplicationsQuery());
            if (result.IsNullOrEmpty())
            {
                return NotFound("There isn't Any Applications.");
            }
            //return Ok(result);
            var res = result.Select(app => new
            {
                app.appId,
                app.appSlug,
                appName = _translationService.GetTranslation(app.appName, lang),
                app.appContent,
                app.appIcon
            }).ToList();
            return Ok(res);
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
            var query = new GetApplicationBySlugQuery(slug);
            var result = await _mediator.Send(query);
            if (result == null)
            {
                return NotFound($"Application With Slug \"{slug}\" Not Found");
            }
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromForm] CreateApplicationDTO application, IFormFile file)
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
        public async Task<IActionResult> Patch(int id, [FromForm] string? jsonPatch, IFormFile? file)
        {
            if (string.IsNullOrEmpty(jsonPatch) && file == null)
            {
                return BadRequest("Patch Document or File must be imported.");
            }
            if (!string.IsNullOrEmpty(jsonPatch))
            {
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
            if (file != null)
            {
                var command = new UpdateApplicationCommand(id, null, file);
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
