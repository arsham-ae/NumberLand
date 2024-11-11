using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Query.Page.Query
{
    public class GetPageCategoryByIdQuery : IRequest<PageCategoryDTO>
    {
        public int Id { get; set; }
        public GetPageCategoryByIdQuery(int id)
        {
            Id = id;
        }
    }
}
