using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using NumberLand.Command.Author;
using NumberLand.Command.Author.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Blogs;
using NumberLand.Query.Author;
using NumberLand.Query.Author.Query;
using NumberLand.Utility;

namespace NumberLand.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public AuthorController(IUnitOfWork unitOfWork, IWebHostEnvironment environment, IMapper mapper, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _environment = environment;
            _mapper = mapper;
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllAuthorsQuery();
            var getall = await _mediator.Send(query);
            if (getall.IsNullOrEmpty())
            {
                return NotFound("There isn't Any Authors!");
            }
            return Ok(getall);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == null || id == 0)
            {
                return BadRequest("Invalid Id!");
            }
            var query = new GetAuthorByIdQuery(id);
            var get = await _mediator.Send(query);
            if (get == null)
            {
                return NotFound($"Author with id {id} Not Found!");
            }
            return Ok(get);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateAuthorDTO author, [FromForm] IFormFile file)
        {
            if (author == null || author.authorId != 0)
            {
                return BadRequest();
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

        [HttpPatch("{id}/UpdateImage")]
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
