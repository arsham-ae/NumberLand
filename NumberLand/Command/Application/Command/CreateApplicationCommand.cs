using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Command.Application.Command
{
    public class CreateApplicationCommand : IRequest<CommandsResponse<ApplicationDTO>>
    {
        public CreateApplicationDTO Application { get; set; }
        public IFormFile File { get; set; }
        public CreateApplicationCommand(CreateApplicationDTO application, IFormFile file)
        {
            Application = application;
            File = file;
        }
    }
}
