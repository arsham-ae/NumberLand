using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Query.Country.Query
{
    public class GetCountryByIdQuery : IRequest<CountryDTO>
    {
        public int Id { get; set; }
        public GetCountryByIdQuery(int id)
        {
            Id = id;
        }
    }
}
