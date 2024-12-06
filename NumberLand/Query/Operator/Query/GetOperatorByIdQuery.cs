using MediatR;
using NumberLand.DataAccess.DTOs;
using NumberLand.Models.Numbers;

namespace NumberLand.Query.Operator.Query
{
    public class GetOperatorByIdQuery : IRequest<OperatorDTO>
    {
        public int Id { get; }

        public GetOperatorByIdQuery(int id)
        {
            Id = id;
        }
    }
}
