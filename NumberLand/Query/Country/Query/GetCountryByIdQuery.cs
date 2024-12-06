using MediatR;
using NumberLand.DataAccess.DTOs;
using NumberLand.Models.Numbers;

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
