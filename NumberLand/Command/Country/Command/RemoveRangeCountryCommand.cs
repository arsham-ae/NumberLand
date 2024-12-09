using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Command.Country.Command
{
    public class RemoveRangeCountryCommand : IRequest<CommandsResponse<CountryDTO>>
    {
        public List<int> Ids { get; set; }
        public RemoveRangeCountryCommand(List<int> ids)
        {
            Ids = ids;
        }
    }
}
