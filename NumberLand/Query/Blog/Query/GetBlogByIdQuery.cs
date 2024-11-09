using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Query.Blog.Query
{
    public class GetBlogByIdQuery : IRequest<BlogDTO>
    {
        public int Id { get; set; }
        public GetBlogByIdQuery(int id)
        {
            Id = id;
        }
    }
}
