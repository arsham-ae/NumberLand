using MediatR;
using NumberLand.Command.Blog.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;

namespace NumberLand.Command.Blog.Handler
{
    public class RemoveBlogHandler : IRequestHandler<RemoveBlogCommand, CommandsResponse<CreateBlogDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _environment;
        public RemoveBlogHandler(IUnitOfWork unitOfWork, IWebHostEnvironment environment)
        {
            _unitOfWork = unitOfWork;
            _environment = environment;
        }
        public async Task<CommandsResponse<CreateBlogDTO>> Handle(RemoveBlogCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var blog = await _unitOfWork.blog.Get(o => o.id == request.Id);
                if (blog == null)
                {
                    return new CommandsResponse<CreateBlogDTO>
                    {
                        status = "Fail",
                        message = $"Blog With Id {request.Id} Not Found!"
                    };
                }
                var fullPath = Path.Combine(_environment.WebRootPath, blog.featuredImagePath);
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
                else
                {
                    throw new FileNotFoundException("File not found.", fullPath);
                }
                _unitOfWork.blog.Delete(blog);
                await _unitOfWork.Save();

                return new CommandsResponse<CreateBlogDTO>
                {
                    status = "Success",
                    message = $"Blog With Id {request.Id} Deleted Successfully."
                };
            }
            catch (Exception ex)
            {
                return new CommandsResponse<CreateBlogDTO>
                {
                    status = "Fail",
                    message = ex.InnerException.Message
                };
            }
        }
    }
}
