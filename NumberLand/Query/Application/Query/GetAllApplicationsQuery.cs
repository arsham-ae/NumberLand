using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Query.Application.Query
{
    public class GetAllApplicationsQuery : IRequest<IEnumerable<ApplicationDTO>>
    {
    }
}
