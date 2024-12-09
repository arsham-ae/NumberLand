using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Query.Country.Query
{
    public class GetAllCountriesQuery : IRequest<IEnumerable<CountryDTO>>
    {
    }
}
