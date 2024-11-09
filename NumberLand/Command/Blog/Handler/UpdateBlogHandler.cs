using AutoMapper;
using Markdig;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NumberLand.Command.Blog.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Blogs;
using NumberLand.Utility;
using System.Reflection.Metadata;

namespace NumberLand.Command.Blog.Handler
{
    public class UpdateBlogHandler : IRequestHandler<UpdateBlogCommand, CommandsResponse<CreateBlogDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;

        public UpdateBlogHandler(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment environment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _environment = environment;
        }
        public async Task<CommandsResponse<CreateBlogDTO>> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
        {
            var pipeLine = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            var blog = await _unitOfWork.blog.Get(p => p.id == request.Id);
            if (blog == null)
            {
                return new CommandsResponse<CreateBlogDTO>
                {
                    status = "Fail",
                    message = "Blog Not Found!"
                };
            }
            if (request.File != null && request.File.Length > 0)
            {
                blog.featuredImagePath = await SaveAuthorImage(request.File);
            }
            try
            {
                request.PatchDoc.ApplyTo(blog);

            }
            catch (Exception ex)
            {
                return new CommandsResponse<CreateBlogDTO>
                {
                    status = "Fail",
                    message = $"Error applying patch document: {ex.Message}"
                };
            }
            blog.slug = SlugHelper.GenerateSlug(blog.title);
            await _unitOfWork.Save();
            return new CommandsResponse<CreateBlogDTO>
            {
                status = "Success",
                message = "Blog Updated Successfully.",
                data = _mapper.Map<CreateBlogDTO>(blog)
            };
        }

        private async Task<string> SaveAuthorImage(IFormFile file)
        {
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

            return Path.Combine("images/blogs", uniqueFileName).Replace("\\", "/");
        }
    }
}
