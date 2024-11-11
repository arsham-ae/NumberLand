using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Command.Page.Command
{
    public class CreatePageCommand : IRequest<CommandsResponse<PageDTO>>
    {
        public CreatePageDTO PageDto { get; set; }
        public CreatePageCommand(CreatePageDTO pageDTO)
        {
            PageDto = pageDTO;
        }
    }
}
