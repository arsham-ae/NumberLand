using MediatR;
using NumberLand.DataAccess.DTOs;
using NumberLand.Models.Numbers;

namespace NumberLand.Query.Application.Query
{
    public class GetAllApplicationsQuery : IRequest<IEnumerable<ApplicationDTO>>
    {
    }
}
