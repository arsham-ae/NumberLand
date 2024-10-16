using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Numbers;
using NumberLand.Utility;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NumberLand.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumberController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public NumberController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var getall = _mapper.Map<List<NumberDTO>>(_unitOfWork.number.GetAll(includeProp: "category,nOperator"));
            return Ok(getall);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var get = _unitOfWork.number.Get(n => n.id == id, includeProp: "category,nOperator");
            return Ok(get);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateNumberDTO number)
        {
            if (number == null || number.id != 0)
            {
                return BadRequest();
            }
            var mappedNum = _mapper.Map<NumberModel>(number);
            mappedNum.slug = SlugHelper.GenerateSlug(number.number);
            _unitOfWork.number.Add(mappedNum);
            _unitOfWork.Save();
            return Ok(number);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, CreateNumberDTO number)
        {
            if (number == null || id == 0)
            {
                BadRequest();
            }
            var mappedNum = _mapper.Map<NumberModel>(number);
            mappedNum.slug = SlugHelper.GenerateSlug(number.number);
            _unitOfWork.number.Update(mappedNum);
            return Ok(number);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<NumberModel> patchDoc)
        {
            _unitOfWork.number.Patch(id, patchDoc);
            return Ok(patchDoc);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            var get = _unitOfWork.number.Get(o => o.id == id);
            _unitOfWork.number.Delete(get);
            _unitOfWork.Save();
            return Ok(get);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveRange([FromBody] List<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return BadRequest();
            }
            var get = _unitOfWork.number.GetAll().Where(p => ids.Contains(p.id)).ToList();
            _unitOfWork.number.DeleteRange(get);
            _unitOfWork.Save();
            return Ok(get);
        }
    }
}
