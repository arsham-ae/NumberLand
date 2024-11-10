using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Command.Blog.Command
{
    public class RemoveRangeBlogCategoryCommand : IRequest<CommandsResponse<BlogCategoryDTO>>
    {
        public List<int> Ids { get; set; }
        public RemoveRangeBlogCategoryCommand(List<int> ids)
        {
            Ids = ids;
        }
    }
}
