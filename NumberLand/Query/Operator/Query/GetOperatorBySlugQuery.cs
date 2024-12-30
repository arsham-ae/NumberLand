using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Query.Operator.Query
{
    public class GetOperatorBySlugQuery : IRequest<OperatorDTO>
    {
        public string Slug { get; set; }
        public GetOperatorBySlugQuery(string slug)
        {
            Slug = slug;
        }
    }
}
