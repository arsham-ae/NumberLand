using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Query.Page.Query
{
    public class GetAllPagesCategoryQuery : IRequest<IEnumerable<PageCategoryDTO>>
    {
    }
}
