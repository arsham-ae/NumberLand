using MediatR;
using NumberLand.Command.Application.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Numbers;

namespace NumberLand.Command.Application.Handler
{
    public class RemoveRangeApplicationHandler : IRequestHandler<RemoveRangeApplicationCommand, CommandsResponse<ApplicationDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveRangeApplicationHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandsResponse<ApplicationDTO>> Handle(RemoveRangeApplicationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var getApps = (await _unitOfWork.application.GetAll()).Where(p => request.Ids.Contains(p.id)).ToList();
                if (!getApps.Any() || getApps == null)
                {
                    return new CommandsResponse<ApplicationDTO>
                    {
                        status = "Fail",
                        message = "Applications Not Found!",
                    };
                }
                _unitOfWork.application.DeleteRange(getApps);
                await _unitOfWork.Save();
                return new CommandsResponse<ApplicationDTO>
                {
                    status = "Success",
                    message = "Applications Deleted Successfully.",
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
