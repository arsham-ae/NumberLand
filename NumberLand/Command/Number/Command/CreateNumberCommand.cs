using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Command.Number.Command
{
    public class CreateNumberCommand : IRequest<CommandsResponse<NumberDTO>>
    {
        public CreateNumberDTO NumberDTO { get; set; }
        public CreateNumberCommand(CreateNumberDTO numberDTO)
        {
            NumberDTO = numberDTO;
        }
    }
}
