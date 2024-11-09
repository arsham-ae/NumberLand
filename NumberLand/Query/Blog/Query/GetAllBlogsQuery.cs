using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Query.Blog.Query
{
    public class GetAllBlogsQuery : IRequest<IEnumerable<BlogDTO>>
    {
    }
}
