using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Command.Blog.Command
{
    public class CreateBlogCategoryCommand : IRequest<CommandsResponse<BlogCategoryDTO>>
    {
        public CreateBlogCategoryDTO BlogCatDTO { get; set; }
        public IFormFile File { get; set; }
        public CreateBlogCategoryCommand(CreateBlogCategoryDTO blogCatDTO, IFormFile file)
        {
            BlogCatDTO = blogCatDTO;
            File = file;
        }
    }
}
