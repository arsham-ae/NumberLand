using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Command.Application.Command
{
    public class UpdateApplicationImageCommand : IRequest<CommandsResponse<ApplicationDTO>>
    {
        public int Id { get; set; }
        public IFormFile File { get; set; }
        public UpdateApplicationImageCommand(int id, IFormFile file)
        {
            Id = id;
            File = file;
        }
    }
}
