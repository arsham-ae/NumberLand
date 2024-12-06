using MediatR;
using NumberLand.Command.Blog.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;

namespace NumberLand.Command.Blog.Handler
{
    public class RemoveRangeBlogHandler : IRequestHandler<RemoveRangeBlogCommand, CommandsResponse<CreateBlogDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveRangeBlogHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandsResponse<CreateBlogDTO>> Handle(RemoveRangeBlogCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var get = (await _unitOfWork.blog.GetAll()).Where(p => request.Ids.Contains(p.id)).ToList();
                if (!get.Any())
                {
                    return new CommandsResponse<CreateBlogDTO>
                    {
                        status = "Fail",
                        message = $"Blogs With Id {string.Join(",", request.Ids)} Not Found!"
                    };
                }
                _unitOfWork.blog.DeleteRange(get);
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
