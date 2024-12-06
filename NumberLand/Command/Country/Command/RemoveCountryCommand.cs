using MediatR;
using NumberLand.DataAccess.DTOs;
using NumberLand.Models.Numbers;

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
