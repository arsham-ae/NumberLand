using AutoMapper;
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
        private readonly SaveImageHelper _saveImageHelper;

        public UpdateBlogHandler(IUnitOfWork unitOfWork, IMapper mapper, SaveImageHelper saveImageHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _saveImageHelper = saveImageHelper;
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
                        message = $"Blog With Id {request.Id} Not Found!"
                    };
                }
                if (request.File != null && request.File.Length > 0)
                {
                    blog.featuredImagePath = await _saveImageHelper.SaveImage(request.File, "blogs");
                }
                request.PatchDoc.ApplyTo(blog);
                blog.content = Markdown.ToHtml(blog.content, pipeLine);
                await _unitOfWork.Save();
                return new CommandsResponse<BlogDTO>
                {
                    status = "Success",
                    message = $"Blog With Id {request.Id} Updated SuccessFully!",
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
    }
}
