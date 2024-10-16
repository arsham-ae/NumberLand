using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Numbers;
using NumberLand.Utility;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NumberLand.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperatorController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public OperatorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var getall = _unitOfWork.nOperator.GetAll();
            return Ok(getall);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            var get = _unitOfWork.nOperator.Get(o => o.id == id);
            return Ok(get);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OperatorModel operatorModel)
        {
            if (operatorModel == null || operatorModel.id != 0)
            {
                return BadRequest();
            }
            operatorModel.slug = SlugHelper.GenerateSlug(operatorModel.operatorCode);
            _unitOfWork.nOperator.Add(operatorModel);
            _unitOfWork.Save();
            return Ok(operatorModel);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, OperatorModel operatorModel)
        {
            if (operatorModel == null || operatorModel.id == 0)
            {
                return BadRequest();
            }
            operatorModel.slug = SlugHelper.GenerateSlug(operatorModel.operatorCode);
            _unitOfWork.nOperator.Update(operatorModel);
            return Ok(operatorModel);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<OperatorModel> patchDoc)
        {
            _unitOfWork.nOperator.Patch(id, patchDoc);
            return Ok(patchDoc);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            var get = _unitOfWork.nOperator.Get(o => o.id == id);
            _unitOfWork.nOperator.Delete(get);
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
            var get = _unitOfWork.nOperator.GetAll().Where(p => ids.Contains(p.id)).ToList();
            _unitOfWork.nOperator.DeleteRange(get);
            _unitOfWork.Save();
            return Ok(get);
        }
    }
}
