using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Command.Blog.Command
{
    public class CreateBlogCategoryCommand : IRequest<CommandsResponse<BlogCategoryDTO>>
    {
        public BlogCategoryDTO BlogDTO { get; set; }
        public CreateBlogCategoryCommand(BlogCategoryDTO blogDTO)
        {
            BlogDTO = blogDTO;
        }
    }
}
