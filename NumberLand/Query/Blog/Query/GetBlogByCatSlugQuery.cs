using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Query.Blog.Query
{
    public class GetBlogByCatSlugQuery : IRequest<IEnumerable<BlogDTO>>
    {
        public string CatSlug { get; set; }
        public GetBlogByCatSlugQuery(string catSlug)
        {
            CatSlug = catSlug;
        }
    }
}
