using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Query.Operator.Query
{
    public class GetAllOperatorsQuery : IRequest<IEnumerable<OperatorDTO>>
    {
    }
}
