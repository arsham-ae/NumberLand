using AutoMapper;
using Markdig;
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
    public class BlogController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<BlogController> _logger;
        private readonly IWebHostEnvironment _environment;
        public BlogController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<BlogController> logger, IWebHostEnvironment environment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _environment = environment;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var getall = _mapper.Map<List<BlogDTO>>(await _unitOfWork.blog.GetAll(includeProp: "author, blogCategories.category"));
            return Ok(getall);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            var get = _mapper.Map<BlogDTO>(await _unitOfWork.blog.Get(o => o.id == id, includeProp: "author, blogCategories.category"));
            return Ok(get);
        }
        [HttpGet("SearchByCategory/{Catid}")]
        public async Task<IActionResult> GetByCategory(int Catid)
        {
            if (Catid == null || Catid == 0)
            {
                return BadRequest();
            }
            var get = _mapper.Map<List<BlogDTO>>((await _unitOfWork.blog.GetAll(includeProp: "author, blogCategories.category"))
        .Where(b => b.blogCategories != null && b.blogCategories.Any(bc => bc.categoryId == Catid))
        .ToList());
            return Ok(get);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateBlogDTO blog, [FromForm] IFormFile file)
        {
            if (blog == null || blog.blogId != 0)
            {
                return BadRequest("Blog Id Should Be 0!");
            }
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file was uploaded.");
            }

            var uploadsFolder = Path.Combine(_environment.WebRootPath, "images", "blogs");
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

            var image = Path.Combine("images/blogs", uniqueFileName);

            var pipeLine = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            blog.blogContent = Markdown.ToHtml(blog.blogContent, pipeLine);
            var mappedBlog = _mapper.Map<BlogModel>(blog);
            mappedBlog.featuredImagePath = image.Replace("\\", "/");
            mappedBlog.slug = SlugHelper.GenerateSlug(blog.blogTitle);
            mappedBlog.createAt = DateTime.Now;
            mappedBlog.updateAt = DateTime.Now;
            mappedBlog.publishedAt = DateTime.Now;
            mappedBlog.blogCategories = new List<BlogCategoryJoinModel>();
            foreach (var categoryId in blog.blogCategories)
            {
                mappedBlog.blogCategories.Add(new BlogCategoryJoinModel
                {
                    categoryId = categoryId
                });
            }


            await _unitOfWork.blog.Add(mappedBlog);
            await _unitOfWork.Save();
            return Ok(blog);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] CreateBlogDTO blog, [FromForm] IFormFile file)
        {
            if (blog == null || blog.blogId == 0)
            {
                return BadRequest();
            }
            if (blog == null || blog.blogId != 0)
            {
                return BadRequest();
            }
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file was uploaded.");
            }

            var uploadsFolder = Path.Combine(_environment.WebRootPath, "images", "uploads");
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

            var image = Path.Combine("images/uploads", uniqueFileName);
            var mappedBlog = _mapper.Map<BlogModel>(blog);
            mappedBlog.featuredImagePath = image.Replace("\\", "/");
            mappedBlog.slug = SlugHelper.GenerateSlug(blog.blogTitle);
            mappedBlog.blogCategories = new List<BlogCategoryJoinModel>();
            foreach (var categoryId in blog.blogCategories)
            {
                mappedBlog.blogCategories.Add(new BlogCategoryJoinModel
                {
                    categoryId = categoryId
                });
            }
            _unitOfWork.blog.Update(mappedBlog);
            return Ok(blog);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<BlogModel> patchDoc)
        {
            _unitOfWork.blog.Patch(id, patchDoc);
            return Ok(patchDoc);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            var get = await _unitOfWork.blog.Get(o => o.id == id);
            _unitOfWork.blog.Delete(get);
            await _unitOfWork.Save();
            return Ok(get);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveRange([FromBody] List<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return BadRequest();
            }
            var get = (await _unitOfWork.blog.GetAll()).Where(p => ids.Contains(p.id)).ToList();
            _unitOfWork.blog.DeleteRange(get);
            await _unitOfWork.Save();
            return Ok(get);
        }

        //[HttpPost("UploadImage")]
        //public async Task<IActionResult> UploadImage(IFormFile file, int blogId)
        //{
        //    if (file == null || file.Length == 0)
        //    {
        //        return BadRequest("No file was uploaded.");
        //    }

        //    var blog = _unitOfWork.blog.Get(b => b.id == blogId);
        //    if (blog == null)
        //    {
        //        return NotFound("Blog not found.");
        //    }

        //    var uploadsFolder = Path.Combine(_environment.WebRootPath, "images", "uploads");
        //    if (!Directory.Exists(uploadsFolder))
        //    {
        //        Directory.CreateDirectory(uploadsFolder);
        //    }
        //    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        //    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        //    using (var fileStream = new FileStream(filePath, FileMode.Create))
        //    {
        //        await file.CopyToAsync(fileStream);
        //    }

        //    var image = Path.Combine("images/uploads", uniqueFileName);
        //    blog.featuredImagePath = image.Replace("\\", "/");
        //    //blog.featuredImagePath = $"{Request.Scheme}://{Request.Host}/{image}";

        //    _unitOfWork.blog.Update(blog);
        //    _unitOfWork.Save();

        //    return Ok(blog.featuredImagePath);
        //}


        [HttpGet("Category")]
        public async Task<IActionResult> CatGetAll()
        {
            var getall = _mapper.Map<List<BlogCategoryDTO>>(await _unitOfWork.blogCategory.GetAll());
            return Ok(getall);
        }
        [HttpGet("Category/{id}")]
        public async Task<IActionResult> CatGet(int id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            var get = _mapper.Map<BlogCategoryDTO>(await _unitOfWork.blogCategory.Get(o => o.id == id));
            return Ok(get);
        }

        [HttpPost("Category")]
        public async Task<IActionResult> CatCreate(BlogCategoryDTO blogCategory)
        {
            if (blogCategory == null || blogCategory.blogCategoryId != 0)
            {
                return BadRequest();
            }
            var mappedCat = _mapper.Map<BlogCategoryModel>(blogCategory);
            mappedCat.slug = SlugHelper.GenerateSlug(blogCategory.blogCategoryName);
            await _unitOfWork.blogCategory.Add(mappedCat);
            await _unitOfWork.Save();
            return Ok(blogCategory);

        }

        [HttpPut("Category/{id}")]
        public async Task<IActionResult> CatEdit(int id, BlogCategoryDTO blogCategory)
        {
            if (blogCategory == null || blogCategory.blogCategoryId == 0)
            {
                return BadRequest();
            }
            var mappedCat = _mapper.Map<BlogCategoryModel>(blogCategory);
            mappedCat.slug = SlugHelper.GenerateSlug(blogCategory.blogCategoryName);
            _unitOfWork.blogCategory.Update(mappedCat);
            return Ok(blogCategory);
        }

        [HttpPatch("Category/{id}")]
        public async Task<IActionResult> CatPatch(int id, [FromBody] JsonPatchDocument<BlogCategoryModel> patchDoc)
        {
            _unitOfWork.blogCategory.Patch(id, patchDoc);
            return Ok(patchDoc);
        }

        [HttpDelete("Category/{id}")]
        public async Task<IActionResult> CatRemove(int id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            var get = await _unitOfWork.blogCategory.Get(o => o.id == id);
            _unitOfWork.blogCategory.Delete(get);
            await _unitOfWork.Save();
            return Ok(get);
        }

        [HttpDelete("Category")]
        public async Task<IActionResult> CatRemoveRange([FromBody] List<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return BadRequest();
            }
            var get = (await _unitOfWork.blogCategory.GetAll()).Where(p => ids.Contains(p.id)).ToList();
            _unitOfWork.blogCategory.DeleteRange(get);
            await _unitOfWork.Save();
            return Ok(get);
        }
    }
}
