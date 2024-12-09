using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Command.Application.Command
{
    public class RemoveRangeApplicationCommand : IRequest<CommandsResponse<ApplicationDTO>>
    {
        public List<int> Ids { get; set; }
        public RemoveRangeApplicationCommand(List<int> ids)
        {
            Ids = ids;
        }
    }
}
