using AutoMapper;
using Markdig;
using MediatR;
using NumberLand.Command.Blog.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Blogs;
using NumberLand.Utility;
using System.Reflection.Metadata;

namespace NumberLand.Command.Blog.Handler
{
    public class CreateBlogHandler : IRequestHandler<CreateBlogCommand, CommandsResponse<CreateBlogDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;

        public CreateBlogHandler(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment environment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _environment = environment;
        }
        public async Task<CommandsResponse<CreateBlogDTO>> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
        {
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "images", "blogs");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(request.File.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await request.File.CopyToAsync(fileStream);
            }

            var image = Path.Combine("images/blogs", uniqueFileName);

            var pipeLine = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            request.BlogDTO.blogContent = Markdown.ToHtml(request.BlogDTO.blogContent, pipeLine);
            var mappedBlog = _mapper.Map<BlogModel>(request.BlogDTO);
            mappedBlog.featuredImagePath = image.Replace("\\", "/");
            mappedBlog.slug = SlugHelper.GenerateSlug(request.BlogDTO.blogTitle);
            mappedBlog.createAt = DateTime.Now;
            mappedBlog.updateAt = DateTime.Now;
            mappedBlog.publishedAt = DateTime.Now;
            mappedBlog.blogCategories = new List<BlogCategoryJoinModel>();
            foreach (var categoryId in request.BlogDTO.blogCategories)
            {
                mappedBlog.blogCategories.Add(new BlogCategoryJoinModel
                {
                    categoryId = categoryId
                });
            }
            await _unitOfWork.blog.Add(mappedBlog);
            await _unitOfWork.Save();

            return new CommandsResponse<CreateBlogDTO>
            {
                status = "Success",
                message = "Blog Created Successfully.",
                data = request.BlogDTO
            };
        }
    }
}
