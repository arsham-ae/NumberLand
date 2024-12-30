using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Query.Country.Query
{
    public class GetCountryBySlugQuery : IRequest<CountryDTO>
    {
        public string Slug { get; set; }
        public GetCountryBySlugQuery(string slug)
        {
            Slug = slug;
        }
    }
}
