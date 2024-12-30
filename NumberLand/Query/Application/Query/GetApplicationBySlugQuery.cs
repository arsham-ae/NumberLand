using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Query.Application.Query
{
    public class GetApplicationBySlugQuery : IRequest<ApplicationDTO>
    {
        public string Slug { get; set; }
        public GetApplicationBySlugQuery(string slug)
        {
            Slug = slug;
        }
    }
}
