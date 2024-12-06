using AutoMapper;
using MediatR;
using NumberLand.Command.Application.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using System.Reflection.Metadata;

namespace NumberLand.Command.Application.Handler
{
    public class UpdateApplicationImageHandler : IRequestHandler<UpdateApplicationImageCommand, CommandsResponse<ApplicationDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;
        public UpdateApplicationImageHandler(IUnitOfWork unitOfWork, IWebHostEnvironment environment, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _environment = environment;
            _mapper = mapper;
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
                        message = $"App With Id {request.Id} Not Found!"
                    };
                }
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "images", "apps");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(request.File.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await request.File.CopyToAsync(fileStream);
                }
                app.appIcon = Path.Combine("images/apps", uniqueFileName).Replace("\\", "/");
                await _unitOfWork.Save();
                return new CommandsResponse<ApplicationDTO>
                {
                    status = "Success",
                    message = "image Updated Successfully.",
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
