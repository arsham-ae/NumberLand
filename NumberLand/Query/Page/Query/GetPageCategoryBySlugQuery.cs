using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Query.Page.Query
{
    public class GetPageCategoryBySlugQuery : IRequest<PageCategoryDTO>
    {
        public string Slug { get; set; }
        public GetPageCategoryBySlugQuery(string slug)
        {
            Slug = slug;
        }
    }
}
