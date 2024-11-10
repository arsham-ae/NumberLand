using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Query.Blog.Query
{
    public class GetAllBlogsCategoryQuery : IRequest<IEnumerable<BlogCategoryDTO>>
    {
    }
}
