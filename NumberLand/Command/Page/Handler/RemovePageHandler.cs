using MediatR;
using NumberLand.Command.Page.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;

namespace NumberLand.Command.Page.Handler
{
    public class RemovePageHandler : IRequestHandler<RemovePageCommand, CommandsResponse<PageDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemovePageHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandsResponse<PageDTO>> Handle(RemovePageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var get = await _unitOfWork.page.Get(o => o.id == request.Id);
                if (get == null)
                {
                    return new CommandsResponse<PageDTO>
                    {
                        status = "Fail",
                        message = $"Page with Id {request.Id} Not Found!"
                    };
                }
                _unitOfWork.page.Delete(get);
                await _unitOfWork.Save();
                return new CommandsResponse<PageDTO>
                {
                    status = "Success",
                    message = $"Page {get.title} with Id {request.Id} Deleted Successfully!"
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
