using MediatR;
using NumberLand.Models.Numbers;
using System.Reflection.Metadata.Ecma335;

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
