using MediatR;
using NumberLand.DataAccess.DTOs;
using NumberLand.Models.Numbers;

namespace NumberLand.Command.Country.Command
{
    public class CreateCountryCommand : IRequest<CommandsResponse<CountryDTO>>
    {
        public CreateCountryDTO Country { get; set; }
        public IFormFile File { get; set; }
        public CreateCountryCommand(CreateCountryDTO country, IFormFile file)
        {
            Country = country;
            File = file;
        }
    }
}
