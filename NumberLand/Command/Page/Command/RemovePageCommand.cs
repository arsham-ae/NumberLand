using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Command.Page.Command
{
    public class RemovePageCommand : IRequest<CommandsResponse<PageDTO>>
    {
        public int Id { get; set; }
        public RemovePageCommand(int id)
        {
            Id = id;
        }
    }
}
