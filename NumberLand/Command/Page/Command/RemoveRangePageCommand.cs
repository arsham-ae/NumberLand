using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Command.Page.Command
{
    public class RemoveRangePageCommand : IRequest<CommandsResponse<PageDTO>>
    {
        public List<int> Ids { get; set; }
        public RemoveRangePageCommand(List<int> ids)
        {
            Ids = ids;
        }
    }
}
