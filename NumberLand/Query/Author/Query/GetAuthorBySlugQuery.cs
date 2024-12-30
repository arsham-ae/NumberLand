using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Query.Author.Query
{
    public class GetAuthorBySlugQuery : IRequest<AuthorDTO>
    {
        public string Slug { get; set; }
        public GetAuthorBySlugQuery(string slug)
        {
            Slug = slug;
        }
    }
}
