using MediatR;
using NumberLand.Command.Blog.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;

namespace NumberLand.Command.Blog.Handler
{
    public class RemoveBlogCategoryHandler : IRequestHandler<RemoveBlogCategoryCommand, CommandsResponse<BlogCategoryDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveBlogCategoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<CommandsResponse<BlogCategoryDTO>> Handle(RemoveBlogCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var get = await _unitOfWork.blogCategory.Get(o => o.id == request.Id);
                if (get == null)
                {
                    return new CommandsResponse<BlogCategoryDTO>
                    {
                        status = "Fail",
                        message = $"BlogCategory With Id {request.Id} Not Found!"
                    };
                }
                _unitOfWork.blogCategory.Delete(get);
                await _unitOfWork.Save();

                return new CommandsResponse<BlogCategoryDTO>
                {
                    status = "Success",
                    message = $"BlogCategory With Id {request.Id} Deleted Successfully."
                };
            }
            catch (Exception ex)
            {
                return new CommandsResponse<BlogCategoryDTO>
                {
                    status = "Fail",
                    message = ex.Message
                };
            }
        }
    }
}
