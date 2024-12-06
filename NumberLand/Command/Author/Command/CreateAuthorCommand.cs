using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Command.Author.Command
{
    public class CreateAuthorCommand : IRequest<CommandsResponse<AuthorDTO>>
    {
        public CreateAuthorDTO authorDTO { get; set; }
        public IFormFile imageFile { get; set; }
        public CreateAuthorCommand(CreateAuthorDTO author, IFormFile file)
        {
            authorDTO = author;
            imageFile = file;
        }
    }
}
