using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Query.Blog.Query
{
    public class GetBlogCategoryByIdQuery : IRequest<BlogCategoryDTO>
    {
        public int Id { get; set; }
        public GetBlogCategoryByIdQuery(int id)
        {
            Id = id;
        }
    }
}
