using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Command.Number.Command
{
    public class UpdateNumberCommand : IRequest<CommandsResponse<NumberDTO>>
    {
        public CreateNumberDTO NumberDto { get; set; }
        public UpdateNumberCommand(CreateNumberDTO numberDTO)
        {
            NumberDto = numberDTO;
        }
    }
}
