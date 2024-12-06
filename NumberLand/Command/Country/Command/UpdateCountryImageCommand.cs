using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Command.Country.Command
{
    public class UpdateCountryImageCommand : IRequest<CommandsResponse<CountryDTO>>
    {
        public int Id { get; set; }
        public IFormFile File { get; set; }
        public UpdateCountryImageCommand(int id, IFormFile file)
        {
            Id = id;
            File = file;
        }
    }
}
