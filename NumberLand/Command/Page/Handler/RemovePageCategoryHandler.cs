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
                var pageCat = await _unitOfWork.pageCategory.Get(pc => pc.id == request.Id);
                if (pageCat == null)
                {
                    return new CommandsResponse<PageCategoryDTO>
                    {
                        status = "Fail",
                        message = $"PageCategory with Id {request.Id} Not Found!"
                    };
                }
                _unitOfWork.pageCategory.Delete(pageCat);
                await _unitOfWork.Save();
                return new CommandsResponse<PageCategoryDTO>
                {
                    status = "Success",
                    message = $"PageCategory {pageCat.name} with Id {request.Id} Deleted Successfully!"
                };
            }
            catch (Exception ex)
            {
                return new CommandsResponse<PageCategoryDTO>
                {
                    status = "Fail",
                    message = ex.InnerException.Message
                };
            }
        }
    }
}
