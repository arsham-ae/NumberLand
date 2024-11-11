using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Command.Page.Command
{
    public class CreatePageCategoryCommand : IRequest<CommandsResponse<PageCategoryDTO>>
    {
        public CreatePageCategoryDTO PageCategory { get; set; }
        public CreatePageCategoryCommand(CreatePageCategoryDTO pageCategory)
        {
            PageCategory = pageCategory;
        }
    }
}
