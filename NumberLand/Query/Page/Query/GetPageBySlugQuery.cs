using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Query.Page.Query
{
    public class GetPageBySlugQuery : IRequest<PageDTO>
    {
        public string Slug { get; set; }
        public GetPageBySlugQuery(string slug)
        {
            Slug = slug;
        }
    }
}
