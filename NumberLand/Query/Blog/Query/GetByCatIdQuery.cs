using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Query.Blog.Query
{
    public class GetByCatIdQuery : IRequest<IEnumerable<BlogDTO>>
    {
        public int CatId { get; set; }
        public GetByCatIdQuery(int catId)
        {
            CatId = catId;
        }
    }
}
