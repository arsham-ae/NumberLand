using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Command.Author.Command
{
    public class CreateAuthorCommand : IRequest<CommandsResponse<AuthorDTO>>
    {
        public CreateAuthorDTO authorDTO { get; }
        public IFormFile imageFile { get; }
        public CreateAuthorCommand(CreateAuthorDTO author, IFormFile file)
        {
            authorDTO = author;
            imageFile = file;
        }
    }
}
