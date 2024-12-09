using MediatR;
using NumberLand.Command.Application.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;

namespace NumberLand.Command.Application.Handler
{
    public class RemoveRangeApplicationHandler : IRequestHandler<RemoveRangeApplicationCommand, CommandsResponse<ApplicationDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _environment;
        public RemoveRangeApplicationHandler(IUnitOfWork unitOfWork, IWebHostEnvironment environment)
        {
            _unitOfWork = unitOfWork;
            _environment = environment;
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
                        message = $"Applications With Id {string.Join(",", request.Ids)} Not Found!"
                    };
                }
                foreach (var app in getApps)
                {
                    var fullPath = Path.Combine(_environment.WebRootPath, app.appIcon);
                    if (File.Exists(fullPath))
                    {
                        File.Delete(fullPath);
                    }
                    else
                    {
                        throw new FileNotFoundException("File not found.", fullPath);
                    }
                }
                _unitOfWork.application.DeleteRange(getApps);
                await _unitOfWork.Save();
                return new CommandsResponse<ApplicationDTO>
                {
                    status = "Success",
                    message = $"Applications With Id {string.Join(",", request.Ids)} Deleted Successfully."
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
