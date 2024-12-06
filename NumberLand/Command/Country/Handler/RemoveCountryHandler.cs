using MediatR;
using NumberLand.Command.Country.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Numbers;

namespace NumberLand.Command.Country.Handler
{
    public class RemoveCountryHandler : IRequestHandler<RemoveCountryCommand, CommandsResponse<CountryDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveCountryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
                        message = "Country Not Found!"
                    };
                }
                _unitOfWork.country.Delete(country);
                await _unitOfWork.Save();
                return new CommandsResponse<CountryDTO>
                {
                    status = "Success",
                    message = "Country Deleted Successfully!"
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
