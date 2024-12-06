using MediatR;
using NumberLand.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.Models.Numbers;

namespace NumberLand.Query.Country.Query
{
    public class GetAllCountriesQuery : IRequest<IEnumerable<CountryDTO>>
    {
    }
}
