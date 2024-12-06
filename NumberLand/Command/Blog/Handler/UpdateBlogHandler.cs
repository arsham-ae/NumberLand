﻿using AutoMapper;
using Markdig;
using MediatR;
using NumberLand.Command.Blog.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Utility;

namespace NumberLand.Command.Blog.Handler
{
    public class UpdateBlogHandler : IRequestHandler<UpdateBlogCommand, CommandsResponse<BlogDTO>>
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
        public async Task<CommandsResponse<BlogDTO>> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var pipeLine = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
                var blog = await _unitOfWork.blog.Get(p => p.id == request.Id);
                if (blog == null)
                {
                    return new CommandsResponse<BlogDTO>
                    {
                        status = "Fail",
                        message = "Blog Not Found!"
                    };
                }
                if (request.File != null && request.File.Length > 0)
                {
                    blog.featuredImagePath = await SaveAuthorImage(request.File);
                }
                request.PatchDoc.ApplyTo(blog);
                blog.content = Markdown.ToHtml(blog.content, pipeLine);
                await _unitOfWork.Save();
                return new CommandsResponse<BlogDTO>
                {
                    status = "Success",
                    message = "Blog Updated Successfully.",
                    data = _mapper.Map<BlogDTO>(await _unitOfWork.blog.Get(b => b.id == blog.id, includeProp: "author, blogCategories.category"))
                };
            }
            catch (Exception ex)
            {
                return new CommandsResponse<BlogDTO>
                {
                    status = "Fail",
                    message = ex.InnerException.Message
                };
            }
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
