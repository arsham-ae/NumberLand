using MediatR;
using NumberLand.Command.Application.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;

namespace NumberLand.Command.Application.Handler
{
    public class RemoveApplicationHandler : IRequestHandler<RemoveApplicationCommand, CommandsResponse<ApplicationDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _environment;
        public RemoveApplicationHandler(IUnitOfWork unitOfWork, IWebHostEnvironment environment)
        {
            _unitOfWork = unitOfWork;
            _environment = environment;
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
                        message = $"Application With Id {request.Id} Not Found!"
                    };
                }
                var fullPath = Path.Combine(_environment.WebRootPath, getApp.appIcon);
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
                else
                {
                    throw new FileNotFoundException("File not found.", fullPath);
                }
                _unitOfWork.application.Delete(getApp);
                await _unitOfWork.Save();
                return new CommandsResponse<ApplicationDTO>
                {
                    status = "Success",
                    message = $"Application With Id {request.Id} Deleted Successfully."
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
