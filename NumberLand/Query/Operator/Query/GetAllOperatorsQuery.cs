using MediatR;
using NumberLand.Models.Numbers;

namespace NumberLand.Query.Operator.Query
{
    public class GetAllOperatorsQuery : IRequest<IEnumerable<OperatorModel>>
    {
    }
}
