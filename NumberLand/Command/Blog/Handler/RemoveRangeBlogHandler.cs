using MediatR;
using NumberLand.Command.Blog.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;

namespace NumberLand.Command.Blog.Handler
{
    public class RemoveRangeBlogHandler : IRequestHandler<RemoveRangeBlogCommand, CommandsResponse<CreateBlogDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _environment;

        public RemoveRangeBlogHandler(IUnitOfWork unitOfWork, IWebHostEnvironment environment)
        {
            _unitOfWork = unitOfWork;
            _environment = environment;
        }

        public async Task<CommandsResponse<CreateBlogDTO>> Handle(RemoveRangeBlogCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var blogs = (await _unitOfWork.blog.GetAll()).Where(p => request.Ids.Contains(p.id)).ToList();
                if (!blogs.Any())
                {
                    return new CommandsResponse<CreateBlogDTO>
                    {
                        status = "Fail",
                        message = $"Blogs With Id {string.Join(",", request.Ids)} Not Found!"
                    };
                }
                foreach (var blog in blogs)
                {
                    var fullPath = Path.Combine(_environment.WebRootPath, blog.featuredImagePath);
                    if (File.Exists(fullPath))
                    {
                        File.Delete(fullPath);
                    }
                    else
                    {
                        throw new FileNotFoundException("File not found.", fullPath);
                    }
                }
                _unitOfWork.blog.DeleteRange(blogs);
                await _unitOfWork.Save();

                return new CommandsResponse<CreateBlogDTO>
                {
                    status = "Success",
                    message = $"Blogs With Id {string.Join(",", request.Ids)} Deleted Successfully."
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
