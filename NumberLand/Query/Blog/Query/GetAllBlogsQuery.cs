using MediatR;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Helper;

namespace NumberLand.Query.Blog.Query
{
    public class GetAllBlogsQuery : IRequest<PaginatedResponse<BlogDTO>>
    {
        public QueryObject Query { get; set; }
        public GetAllBlogsQuery(QueryObject query)
        {
            Query = query;
        }
    }
}
