using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Command.Number.Command
{
    public class DeleteRangeNumberCommand : IRequest<CommandsResponse<NumberDTO>>
    {
        public List<int> Ids { get; set; }
        public DeleteRangeNumberCommand(List<int> ids)
        {
            Ids = ids;
        }
    }
}
