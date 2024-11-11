using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Query.Number.Query
{
    public class GetNumberByIdQuery : IRequest<NumberDTO>
    {
        public int Id { get; }

        public GetNumberByIdQuery(int id)
        {
            Id = id;
        }
    }
}
