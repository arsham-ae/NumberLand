using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Query.Page.Query
{
    public class GetAllPagesQuery : IRequest<IEnumerable<PageDTO>>
    {
    }
}
