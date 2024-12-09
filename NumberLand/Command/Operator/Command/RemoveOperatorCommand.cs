using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Command.Operator.Command
{
    public class RemoveOperatorCommand : IRequest<CommandsResponse<OperatorDTO>>
    {
        public int Id { get; set; }
        public RemoveOperatorCommand(int id)
        {
            Id = id;
        }
    }
}
