using AutoMapper;
using Markdig;
using MediatR;
using NumberLand.Command.Blog.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Blogs;
using NumberLand.Utility;

namespace NumberLand.Command.Blog.Handler
{
    public class CreateBlogHandler : IRequestHandler<CreateBlogCommand, CommandsResponse<BlogDTO>>
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
        public async Task<CommandsResponse<BlogDTO>> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
        {
            try
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
                    var get = await _unitOfWork.blogCategory.Get(bc => bc.id == categoryId);
                    if (get != null)
                    {
                        mappedBlog.blogCategories.Add(new BlogCategoryJoinModel
                        {
                            categoryId = categoryId
                        });
                    }
                    else
                    {
                        return new CommandsResponse<BlogDTO>
                        {
                            status = "Fail",
                            message = "One Or All Of BlogCategories Not Exicted!",
                        };
                    }
                }
                await _unitOfWork.blog.Add(mappedBlog);
                await _unitOfWork.Save();

                return new CommandsResponse<BlogDTO>
                {
                    status = "Success",
                    message = "Blog Created Successfully.",
                    data = _mapper.Map<BlogDTO>(await _unitOfWork.blog.Get(b => b.id == mappedBlog.id, includeProp: "author, blogCategories.category"))
                };
            }
            catch (Exception ex)
            {
                return new CommandsResponse<BlogDTO>
                {
                    status = "Fail",
                    message = ex.Message
                };
            }
        }
    }
}
