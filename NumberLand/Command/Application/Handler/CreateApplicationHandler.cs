using AutoMapper;
using Markdig;
using MediatR;
using NumberLand.Command.Application.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Numbers;
using NumberLand.Utility;

namespace NumberLand.Command.Application.Handler
{
    public class CreateApplicationHandler : IRequestHandler<CreateApplicationCommand, CommandsResponse<ApplicationDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;
        public CreateApplicationHandler(IUnitOfWork unitOfWork, IWebHostEnvironment environment, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _environment = environment;
            _mapper = mapper;
        }

        public async Task<CommandsResponse<ApplicationDTO>> Handle(CreateApplicationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var mappedApp = _mapper.Map<ApplicationModel>(request.Application);
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

                var image = Path.Combine("images/apps", uniqueFileName);
                mappedApp.appIcon = image.Replace("\\", "/");

                if (request.Application.appContent != null)
                {
                    var pipeLine = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
                    mappedApp.content = Markdown.ToHtml(request.Application.appContent, pipeLine);
                }
                mappedApp.slug = SlugHelper.GenerateSlug(request.Application.appSlug);
                await _unitOfWork.application.Add(mappedApp);
                await _unitOfWork.Save();
                return new CommandsResponse<ApplicationDTO>()
                {
                    status = "Success",
                    message = "Application Created Successfully.",
                    data = _mapper.Map<ApplicationDTO>(mappedApp)
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
