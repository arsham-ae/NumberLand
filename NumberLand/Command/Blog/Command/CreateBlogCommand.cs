using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Command.Blog.Command
{
    public class CreateBlogCommand : IRequest<CommandsResponse<CreateBlogDTO>>
    {
        public CreateBlogDTO BlogDTO { get; set; }
        public IFormFile File { get; set; }
        public CreateBlogCommand(CreateBlogDTO blogDTO, IFormFile file)
        {
            BlogDTO = blogDTO;
            File = file;
        }
    }
}
