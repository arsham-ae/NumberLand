using MediatR;
using NumberLand.Models.Numbers;

namespace NumberLand.Query.Operator.Query
{
    public class GetOperatorByIdQuery : IRequest<OperatorModel>
    {
        public int Id { get; }

        public GetOperatorByIdQuery(int id)
        {
            Id = id;
        }
    }
}
