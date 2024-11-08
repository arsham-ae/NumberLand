using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Command.Number.Command
{
    public class DeleteNumberCommand : IRequest<CommandsResponse<NumberDTO>>
    {
        public int id { get; set; }
        public DeleteNumberCommand(int Id)
        {
            id = Id;
        }
    }
}
