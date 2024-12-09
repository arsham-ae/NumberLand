using AutoMapper;
using Markdig;
using MediatR;
using NumberLand.Command.Country.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Numbers;
using NumberLand.Utility;

namespace NumberLand.Command.Country.Handler
{
    public class CreateCountryHandler : IRequestHandler<CreateCountryCommand, CommandsResponse<CountryDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly SaveImageHelper _saveImageHelper;
        public CreateCountryHandler(IUnitOfWork unitOfWork, IMapper mapper, SaveImageHelper saveImageHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _saveImageHelper = saveImageHelper;
        }

        public async Task<CommandsResponse<CountryDTO>> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var mappedCountry = _mapper.Map<CountryModel>(request.Country);
                mappedCountry.flagIcon = await _saveImageHelper.SaveImage(request.File, "flags");
                if (request.Country.countryContent != null)
                {
                    var pipeLine = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
                    mappedCountry.content = Markdown.ToHtml(request.Country.countryContent, pipeLine);
                }
                mappedCountry.slug = SlugHelper.GenerateSlug(request.Country.countrySlug);
                await _unitOfWork.country.Add(mappedCountry);
                await _unitOfWork.Save();
                return new CommandsResponse<CountryDTO>()
                {
                    status = "Success",
                    message = "Country Created Successfully.",
                    data = _mapper.Map<CountryDTO>(mappedCountry)
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
