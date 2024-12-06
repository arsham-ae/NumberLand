using MediatR;
using NumberLand.DataAccess.DTOs;
using NumberLand.Models.Numbers;

namespace NumberLand.Query.Operator.Query
{
    public class GetAllOperatorsQuery : IRequest<IEnumerable<OperatorDTO>>
    {
    }
}
