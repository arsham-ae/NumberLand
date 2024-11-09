using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Command.Blog.Command
{
    public class RemoveRangeBlogCommand : IRequest<CommandsResponse<CreateBlogDTO>>
    {
        public List<int> Ids { get; set; }
        public RemoveRangeBlogCommand(List<int> ids)
        {
            Ids = ids;
        }
    }
}
