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
        private readonly IMapper _mapper;
        private readonly SaveImageHelper _saveImageHelper;
        public CreateApplicationHandler(IUnitOfWork unitOfWork, IMapper mapper, SaveImageHelper saveImageHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _saveImageHelper = saveImageHelper;
        }
        public async Task<CommandsResponse<ApplicationDTO>> Handle(CreateApplicationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var mappedApp = _mapper.Map<ApplicationModel>(request.Application);
                mappedApp.appIcon = await _saveImageHelper.SaveImage(request.File, "apps");
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
