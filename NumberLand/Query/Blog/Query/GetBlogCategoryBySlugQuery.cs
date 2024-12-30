using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Query.Blog.Query
{
    public class GetBlogCategoryBySlugQuery : IRequest<BlogCategoryDTO>
    {
        public string Slug { get; set; }
        public GetBlogCategoryBySlugQuery(string slug)
        {
            Slug = slug;
        }
    }
}
