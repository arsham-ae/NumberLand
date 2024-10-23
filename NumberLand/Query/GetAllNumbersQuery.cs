using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Query
{
    public class GetAllNumbersQuery : IRequest<List<NumberDTO>>
    {
    }
}
