using MediatR;
using NumberLand.Command.Country.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Numbers;

namespace NumberLand.Command.Country.Handler
{
    public class RemoveRangeCountryHandler : IRequestHandler<RemoveRangeCountryCommand, CommandsResponse<CountryDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveRangeCountryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandsResponse<CountryDTO>> Handle(RemoveRangeCountryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var countries = (await _unitOfWork.country.GetAll()).Where(p => request.Ids.Contains(p.id)).ToList();
                if (!countries.Any() || countries == null)
                {
                    return new CommandsResponse<CountryDTO>
                    {
                        status = "Fail",
                        message = $"Countries With Id {string.Join(",", request.Ids)} Not Found!",
                    };
                }
                _unitOfWork.country.DeleteRange(countries);
                await _unitOfWork.Save();
                return new CommandsResponse<CountryDTO>
                {
                    status = "Success",
                    message = $"Countries With Id {string.Join(",", request.Ids)} Deleted Successfully.",
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
