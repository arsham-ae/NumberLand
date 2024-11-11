using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Command.Author.Command
{
    public class RemoveRangeAuthorCommand : IRequest<CommandsResponse<AuthorDTO>>
    {
        public List<int> Ids { get; set; }
        public RemoveRangeAuthorCommand(List<int> ids)
        {
            Ids = ids;
        }
    }
}
