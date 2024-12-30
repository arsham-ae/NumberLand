using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Query.Number.Query
{
    public class GetNumberBySlugQuery : IRequest<NumberDTO>
    {
        public string Slug { get; set; }
        public GetNumberBySlugQuery(string slug)
        {
            Slug = slug;
        }
    }
}
