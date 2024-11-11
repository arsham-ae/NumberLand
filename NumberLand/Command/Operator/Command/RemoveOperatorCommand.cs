using MediatR;
using NumberLand.Models.Numbers;

namespace NumberLand.Command.Operator.Command
{
    public class RemoveOperatorCommand : IRequest<CommandsResponse<OperatorModel>>
    {
        public int Id { get; set; }
        public RemoveOperatorCommand(int id)
        {
            Id = id;
        }
    }
}
