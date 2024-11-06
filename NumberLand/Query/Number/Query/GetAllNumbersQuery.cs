using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Query.Number.Query
{
    public class GetAllNumbersQuery : IRequest<List<NumberDTO>>
    {
    }
}
