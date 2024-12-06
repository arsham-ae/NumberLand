using MediatR;
using NumberLand.DataAccess.DTOs;
using NumberLand.Models.Numbers;

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
