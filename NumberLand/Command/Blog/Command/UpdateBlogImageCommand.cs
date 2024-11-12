using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Command.Blog.Command
{
    public class UpdateBlogImageCommand : IRequest<CommandsResponse<BlogDTO>>
    {
        public int Id { get; set; }
        public IFormFile File { get; set; }
        public UpdateBlogImageCommand(int id, IFormFile file)
        {
            Id = id;
            File = file;
        }
    }
}
