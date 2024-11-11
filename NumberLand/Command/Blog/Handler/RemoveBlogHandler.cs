using MediatR;
using NumberLand.Command.Blog.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;

namespace NumberLand.Command.Blog.Handler
{
    public class RemoveBlogHandler : IRequestHandler<RemoveBlogCommand, CommandsResponse<CreateBlogDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveBlogHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<CommandsResponse<CreateBlogDTO>> Handle(RemoveBlogCommand request, CancellationToken cancellationToken)
        {
            var get = await _unitOfWork.blog.Get(o => o.id == request.Id);
            if (get == null)
            {
                return new CommandsResponse<CreateBlogDTO>
                {
                    status = "Fail",
                    message = $"Blog With Id {request.Id} Not Found!"
                };
            }
            _unitOfWork.blog.Delete(get);
            await _unitOfWork.Save();

            return new CommandsResponse<CreateBlogDTO>
            {
                status = "Success",
                message = $"Blog With Id {request.Id} Deleted Successfully."
            };
        }
    }
}
