using AutoMapper;
using Markdig;
using MediatR;
using NumberLand.Command.Page.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Pages;
using NumberLand.Utility;

namespace NumberLand.Command.Page.Handler
{
    public class CreatePageHandler : IRequestHandler<CreatePageCommand, CommandsResponse<PageDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreatePageHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CommandsResponse<PageDTO>> Handle(CreatePageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var mappedPage = _mapper.Map<PageeModel>(request.PageDto);
                var pipeLine = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
                mappedPage.content = Markdown.ToHtml(mappedPage.content, pipeLine);
                mappedPage.slug = SlugHelper.GenerateSlug(mappedPage.slug);
                await _unitOfWork.page.Add(mappedPage);
                await _unitOfWork.Save();
                return new CommandsResponse<PageDTO>
                {
                    status = "Success",
                    message = "Page Created Successfully.",
                    data = _mapper.Map<PageDTO>(await _unitOfWork.page.Get(p => p.id == mappedPage.id))
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
