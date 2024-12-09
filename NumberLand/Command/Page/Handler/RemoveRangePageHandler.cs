using MediatR;
using NumberLand.Command.Page.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;

namespace NumberLand.Command.Page.Handler
{
    public class RemoveRangePageHandler : IRequestHandler<RemoveRangePageCommand, CommandsResponse<PageDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveRangePageHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandsResponse<PageDTO>> Handle(RemoveRangePageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var pages = (await _unitOfWork.page.GetAll()).Where(p => request.Ids.Contains(p.id)).ToList();
                if (pages == null || !pages.Any())
                {
                    return new CommandsResponse<PageDTO>
                    {
                        status = "Fail",
                        message = $"Pages with Id {string.Join(",", request.Ids)} Not Found!"
                    };
                }
                _unitOfWork.page.DeleteRange(pages);
                await _unitOfWork.Save();
                return new CommandsResponse<PageDTO>
                {
                    status = "Success",
                    message = $"Pages with Id {string.Join(",", request.Ids)} Deleted Successfully!"
                };
            }
            catch (Exception ex)
            {
                return new CommandsResponse<PageDTO>
                {
                    status = "Fail",
                    message = ex.InnerException.Message
                };
            }
        }
    }
}
