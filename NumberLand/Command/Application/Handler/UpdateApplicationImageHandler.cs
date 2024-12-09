using AutoMapper;
using MediatR;
using NumberLand.Command.Application.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Utility;

namespace NumberLand.Command.Application.Handler
{
    public class UpdateApplicationImageHandler : IRequestHandler<UpdateApplicationImageCommand, CommandsResponse<ApplicationDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly SaveImageHelper _saveImageHelper;
        public UpdateApplicationImageHandler(IUnitOfWork unitOfWork, IMapper mapper, SaveImageHelper saveImageHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _saveImageHelper = saveImageHelper;
        }

        public async Task<CommandsResponse<ApplicationDTO>> Handle(UpdateApplicationImageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var app = await _unitOfWork.application.Get(app => app.id == request.Id);
                if (app == null)
                {
                    return new CommandsResponse<ApplicationDTO>
                    {
                        status = "Fail",
                        message = $"Application With Id {request.Id} Not Found!"
                    };
                }
                app.appIcon = await _saveImageHelper.SaveImage(request.File, "apps");
                await _unitOfWork.Save();
                return new CommandsResponse<ApplicationDTO>
                {
                    status = "Success",
                    message = $"Application Image With Id {request.Id} Updated Successfully.",
                    data = _mapper.Map<ApplicationDTO>(app)
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
