using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Command.Author.Command
{
    public class UpdateImageAuthorCommand : IRequest<CommandsResponse<AuthorDTO>>
    {
        public int Id { get; set; }
        public IFormFile File { get; set; }
        public UpdateImageAuthorCommand(int id, IFormFile file)
        {
            Id = id;
            File = file;
        }
    }
}
