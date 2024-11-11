using MediatR;
using NumberLand.Command.Page.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;

namespace NumberLand.Command.Page.Handler
{
    public class RemoveRangePageCategoryHandler : IRequestHandler<RemoveRangePageCategoryCommand, CommandsResponse<PageCategoryDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveRangePageCategoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandsResponse<PageCategoryDTO>> Handle(RemoveRangePageCategoryCommand request, CancellationToken cancellationToken)
        {
            var get = (await _unitOfWork.pageCategory.GetAll()).Where(p => request.Ids.Contains(p.id)).ToList();
            if (get == null || !get.Any())
            {
                return new CommandsResponse<PageCategoryDTO>
                {
                    status = "Fail",
                    message = $"PageCategories with Id {string.Join(",", request.Ids)} Not Found!"
                };
            }
            _unitOfWork.pageCategory.DeleteRange(get);
            await _unitOfWork.Save();
            return new CommandsResponse<PageCategoryDTO>
            {
                status = "Success",
                message = $"PageCategories with Id {string.Join(",", request.Ids)} Deleted Successfully!"
            };
        }
    }
}
