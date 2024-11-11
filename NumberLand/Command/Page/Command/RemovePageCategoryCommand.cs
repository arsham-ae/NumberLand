using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Command.Page.Command
{
    public class RemovePageCategoryCommand : IRequest<CommandsResponse<PageCategoryDTO>>
    {
        public int Id { get; set; }
        public RemovePageCategoryCommand(int id)
        {
            Id = id;
        }
    }
}
