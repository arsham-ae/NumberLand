using MediatR;
using NumberLand.DataAccess.DTOs;
using System.Security.Principal;

namespace NumberLand.Command.Blog.Command
{
    public class RemoveBlogCommand : IRequest<CommandsResponse<CreateBlogDTO>>
    {
        public int Id { get; set; }
        public RemoveBlogCommand(int id)
        {
            Id = id;
        }
    }
}
