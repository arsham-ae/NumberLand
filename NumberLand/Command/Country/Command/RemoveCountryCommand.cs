using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Command.Country.Command
{
    public class RemoveCountryCommand : IRequest<CommandsResponse<CountryDTO>>
    {
        public int Id { get; set; }
        public RemoveCountryCommand(int id)
        {
            Id = id;
        }
    }
}
