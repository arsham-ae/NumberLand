using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Command.Page.Command
{
    public class RemoveRangePageCategoryCommand : IRequest<CommandsResponse<PageCategoryDTO>>
    {
        public List<int> Ids { get; set; }
        public RemoveRangePageCategoryCommand(List<int> ids)
        {
            Ids = ids;
        }
    }
}
