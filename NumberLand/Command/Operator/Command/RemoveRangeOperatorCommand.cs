using MediatR;
using NumberLand.Models.Numbers;

namespace NumberLand.Command.Operator.Command
{
    public class RemoveRangeOperatorCommand : IRequest<CommandsResponse<OperatorModel>>
    {
        public List<int> Ids { get; set; }
        public RemoveRangeOperatorCommand(List<int> ids)
        {
            Ids = ids;
        }
    }
}
