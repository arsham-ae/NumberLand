using MediatR;
using NumberLand.DataAccess.DTOs;
using System.Security.Principal;

namespace NumberLand.Command.Blog.Command
{
    public class RemoveBlogCategoryCommand : IRequest<CommandsResponse<BlogCategoryDTO>>
    {
        public int Id { get; set; }
        public RemoveBlogCategoryCommand(int id)
        {
            Id = id;
        }
    }
}
