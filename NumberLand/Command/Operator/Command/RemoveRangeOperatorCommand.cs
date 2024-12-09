using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Command.Operator.Command
{
    public class RemoveRangeOperatorCommand : IRequest<CommandsResponse<OperatorDTO>>
    {
        public List<int> Ids { get; set; }
        public RemoveRangeOperatorCommand(List<int> ids)
        {
            Ids = ids;
        }
    }
}
