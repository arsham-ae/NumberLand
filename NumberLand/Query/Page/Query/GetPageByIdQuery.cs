using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Query.Page.Query
{
    public class GetPageByIdQuery : IRequest<PageDTO>
    {
        public int Id { get; set; }
        public GetPageByIdQuery(int id)
        {
            Id = id;
        }
    }
}
