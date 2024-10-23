using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NumberLand.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Numbers;
using NumberLand.Query;
using NumberLand.Utility;
using System.ComponentModel.DataAnnotations;

namespace NumberLand.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumberController : ControllerBase
    {
        private readonly IMediator _mediator;
        public NumberController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllNumbersQuery();
            var result = await _mediator.Send(query);
            if (result.IsNullOrEmpty())
            {
                return NotFound("There Isn't Any Number To Show");
            }
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var query = new GetNumberByIdQuery(id);
            var result = await _mediator.Send(query);
            if (result == null)
            {
                return NotFound($"Number With Id {id} Not Found!");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateNumberCommand number)
        {
            var result = await _mediator.Send(number);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, UpdateNumberCommand number)
        {
            if (number == null)
            {
                return BadRequest("Invalid Number Data!");
            }
            var result = await _mediator.Send(number);
            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<NumberModel> patchDoc)
        {
            var result = await _mediator.Send(new PatchNumberCommand(id, patchDoc));
            if (result == null)
            {
                return BadRequest("Invalid Data!");
            }
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            if (id == null || id == 0)
            {
                return BadRequest("Invalid Id!");
            }
            var result = await _mediator.Send(new DeleteNumberCommand(id));
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveRange([FromBody] List<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return BadRequest("Invalid Data!");
            }
            var result = await _mediator.Send(new DeleteRangeNumberCommand(ids));
            return Ok(result);
        }
    }
}
