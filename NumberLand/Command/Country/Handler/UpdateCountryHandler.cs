using AutoMapper;
using Markdig;
using MediatR;
using NumberLand.Command.Country.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Numbers;

namespace NumberLand.Command.Country.Handler
{
    public class UpdateCountryHandler : IRequestHandler<UpdateCountryCommand, CommandsResponse<CountryDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        public UpdateCountryHandler(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment environment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _environment = environment;
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
                        message = "Country Not Found!"
                    };
                }
                if (request.File != null && request.File.Length > 0)
                {
                    country.flagIcon = await SaveFlagIcon(request.File);
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
        private async Task<string> SaveFlagIcon(IFormFile file)
        {
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "images", "flags");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return Path.Combine("images/flags", uniqueFileName).Replace("\\", "/");
        }
    }
}
