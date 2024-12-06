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
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;
        public CreateCountryHandler(IUnitOfWork unitOfWork, IWebHostEnvironment environment, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _environment = environment;
            _mapper = mapper;
        }

        public async Task<CommandsResponse<CountryDTO>> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var mappedCountry = _mapper.Map<CountryModel>(request.Country);
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "images", "flags");
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

                var image = Path.Combine("images/flags", uniqueFileName);
                mappedCountry.flagIcon = image.Replace("\\", "/");

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
