using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Command.Author.Command
{
    public class RemoveAuthorCommand : IRequest<CommandsResponse<AuthorDTO>>
    {
        public int Id { get; set; }
        public RemoveAuthorCommand(int id)
        {
            Id = id;
        }
    }

    public class DeleteAuthorResponse
    {
        public int id { get; set; }
        public string message { get; set; }
    }
}
