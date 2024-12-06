using MediatR;
using NumberLand.DataAccess.DTOs;
using NumberLand.Models.Numbers;

namespace NumberLand.Query.Application.Query
{
    public class GetApplicationByIdQuery : IRequest<ApplicationDTO>
    {
        public int Id { get; set; }
        public GetApplicationByIdQuery(int id)
        {
            Id = id;
        }
    }
}
