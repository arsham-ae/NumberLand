using MediatR;
using NumberLand.Command.Country.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;

namespace NumberLand.Command.Country.Handler
{
    public class RemoveCountryHandler : IRequestHandler<RemoveCountryCommand, CommandsResponse<CountryDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _environment;

        public RemoveCountryHandler(IUnitOfWork unitOfWork, IWebHostEnvironment environment)
        {
            _unitOfWork = unitOfWork;
            _environment = environment;
        }

        public async Task<CommandsResponse<CountryDTO>> Handle(RemoveCountryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var country = await _unitOfWork.country.Get(c => c.id == request.Id);
                if (country == null)
                {
                    return new CommandsResponse<CountryDTO>
                    {
                        status = "Fail",
                        message = $"Country With Id {request.Id} Not Found!"
                    };
                }
                var fullPath = Path.Combine(_environment.WebRootPath, country.flagIcon);
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
                else
                {
                    throw new FileNotFoundException("File not found.", fullPath);
                }
                _unitOfWork.country.Delete(country);
                await _unitOfWork.Save();
                return new CommandsResponse<CountryDTO>
                {
                    status = "Success",
                    message = $"Country With Id {request.Id} Deleted Successfully."
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
