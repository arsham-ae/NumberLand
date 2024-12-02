using MediatR;
using NumberLand.Command.Page.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;

namespace NumberLand.Command.Page.Handler
{
    public class RemovePageCategoryHandler : IRequestHandler<RemovePageCategoryCommand, CommandsResponse<PageCategoryDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemovePageCategoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandsResponse<PageCategoryDTO>> Handle(RemovePageCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var get = await _unitOfWork.pageCategory.Get(pc => pc.id == request.Id);
                if (get == null)
                {
                    return new CommandsResponse<PageCategoryDTO>
                    {
                        status = "Fail",
                        message = $"PageCategory with Id {request.Id} Not Found!"
                    };
                }
                _unitOfWork.pageCategory.Delete(get);
                await _unitOfWork.Save();
                return new CommandsResponse<PageCategoryDTO>
                {
                    status = "Success",
                    message = $"PageCategory {get.name} with Id {request.Id} Deleted Successfully!"
                };
            }
            catch (Exception ex)
            {
                return new CommandsResponse<PageCategoryDTO>
                {
                    status = "Fail",
                    message = ex.Message
                };
            }
        }
    }
}
