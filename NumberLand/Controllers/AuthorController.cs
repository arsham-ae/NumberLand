using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Blogs;
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
        public AuthorController(IUnitOfWork unitOfWork, IWebHostEnvironment environment, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _environment = environment;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var getall = _unitOfWork.author.GetAll();
            return Ok(getall);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            var get = _unitOfWork.author.Get(o => o.id == id);
            return Ok(get);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AuthorDTO author, IFormFile file)
        {
            if (author == null || author.id != 0)
            {
                return BadRequest();
            }
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file was uploaded.");
            }

            var uploadsFolder = Path.Combine(_environment.WebRootPath, "images", "authors");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            var image = Path.Combine("images/authors", uniqueFileName);
            var mappedAuthor = _mapper.Map<AuthorModel>(author);
            mappedAuthor.imagePath = image.Replace("\\", "/");
            mappedAuthor.slug = SlugHelper.GenerateSlug(author.name);
            _unitOfWork.author.Add(mappedAuthor);
            _unitOfWork.Save();
            return Ok(mappedAuthor);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, AuthorDTO author, IFormFile file)
        {
            if (author == null || author.id == 0)
            {
                return BadRequest();
            }
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file was uploaded.");
            }

            var uploadsFolder = Path.Combine(_environment.WebRootPath, "images", "authors");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            var image = Path.Combine("images/authors", uniqueFileName);
            var mappedAuthor = _mapper.Map<AuthorModel>(author);
            mappedAuthor.imagePath = image.Replace("\\", "/");
            mappedAuthor.slug = SlugHelper.GenerateSlug(author.name);
            _unitOfWork.author.Update(mappedAuthor);
            return Ok(author);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<AuthorModel> patchDoc)
        {
            _unitOfWork.author.Patch(id, patchDoc);
            return Ok(patchDoc);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            var get = _unitOfWork.author.Get(o => o.id == id);
            _unitOfWork.author.Delete(get);
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
            var get = _unitOfWork.author.GetAll().Where(p => ids.Contains(p.id)).ToList();
            _unitOfWork.author.DeleteRange(get);
            _unitOfWork.Save();
            return Ok(get);
        }
    }
}
