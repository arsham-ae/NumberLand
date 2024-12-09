using AutoMapper;
using Markdig;
using MediatR;
using NumberLand.Command.Country.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Utility;

namespace NumberLand.Command.Country.Handler
{
    public class UpdateCountryHandler : IRequestHandler<UpdateCountryCommand, CommandsResponse<CountryDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly SaveImageHelper _saveImageHelper;
        public UpdateCountryHandler(IUnitOfWork unitOfWork, IMapper mapper, SaveImageHelper saveImageHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _saveImageHelper = saveImageHelper;
        }

        public async Task<CommandsResponse<CountryDTO>> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var pipeLine = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
                var country = await _unitOfWork.country.Get(c => c.id == request.Id);
                if (country == null)
                {
                    return new CommandsResponse<CountryDTO>
                    {
                        status = "Fail",
                        message = $"Country With Id {request.Id} Not Found!"
                    };
                }
                if (request.File != null && request.File.Length > 0)
                {
                    country.flagIcon = await _saveImageHelper.SaveImage(request.File, "flags");
                }
                request.PatchDoc.ApplyTo(country);
                await _unitOfWork.Save();
                country.content = Markdown.ToHtml(country.content, pipeLine);
                return new CommandsResponse<CountryDTO>
                {
                    status = "Success",
                    message = $"Country With Id {request.Id} Updated SuccessFully!",
                    data = _mapper.Map<CountryDTO>(country)
                };

            }
            catch (Exception ex)
            {
                return new CommandsResponse<CountryDTO>
                {
                    status = "Fail",
                    message = ex.InnerException.Message
                };
            }
        }
    }
}
