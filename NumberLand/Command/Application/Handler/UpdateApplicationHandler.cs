using AutoMapper;
using Markdig;
using MediatR;
using NumberLand.Command.Application.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Utility;

namespace NumberLand.Command.Application.Handler
{
    public class UpdateApplicationHandler : IRequestHandler<UpdateApplicationCommand, CommandsResponse<ApplicationDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly SaveImageHelper _saveImageHelper;
        public UpdateApplicationHandler(IUnitOfWork unitOfWork, IMapper mapper, SaveImageHelper saveImageHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _saveImageHelper = saveImageHelper;
        }

        public async Task<CommandsResponse<ApplicationDTO>> Handle(UpdateApplicationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var pipeLine = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
                var app = await _unitOfWork.application.Get(app => app.id == request.Id);
                if (app == null)
                {
                    return new CommandsResponse<ApplicationDTO>
                    {
                        status = "Fail",
                        message = $"Application With Id {request.Id} Not Found!"
                    };
                }
                if (request.File != null && request.File.Length > 0)
                {
                    app.appIcon = await _saveImageHelper.SaveImage(request.File, "apps");
                }
                request.PatchDoc.ApplyTo(app);
                app.slug = SlugHelper.GenerateSlug2(app.slug);
                app.content = Markdown.ToHtml(app.content, pipeLine);
                await _unitOfWork.Save();

                return new CommandsResponse<ApplicationDTO>
                {
                    status = "Success",
                    message = $"Application With Id {request.Id} Updated SuccessFully!",
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
