using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Command.Blog.Command
{
    public class CreateBlogCategoryCommand : IRequest<CommandsResponse<BlogCategoryDTO>>
    {
        public BlogCategoryDTO BlogCatDTO { get; set; }
        public CreateBlogCategoryCommand(BlogCategoryDTO blogCatDTO)
        {
            BlogCatDTO = blogCatDTO;
        }
    }
}
