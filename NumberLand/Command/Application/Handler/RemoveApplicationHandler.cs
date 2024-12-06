using MediatR;
using NumberLand.Command.Application.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Numbers;

namespace NumberLand.Command.Application.Handler
{
    public class RemoveApplicationHandler : IRequestHandler<RemoveApplicationCommand, CommandsResponse<ApplicationDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveApplicationHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandsResponse<ApplicationDTO>> Handle(RemoveApplicationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var getApp = await _unitOfWork.application.Get(app => app.id == request.Id);
                if (getApp == null)
                {
                    return new CommandsResponse<ApplicationDTO>
                    {
                        status = "Fail",
                        message = "Application Not Found!"
                    };
                }
                _unitOfWork.application.Delete(getApp);
                await _unitOfWork.Save();
                return new CommandsResponse<ApplicationDTO>
                {
                    status = "Success",
                    message = "Application Deleted Successfully!"
                };
            }
            catch (Exception ex)
            {
                return new CommandsResponse<ApplicationDTO>
                {
                    status = "Fail",
                    message = ex.InnerException.Message
                };
            }
        }
    }
}
