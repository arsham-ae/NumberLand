using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Query.Author.Query
{
    public class GetAllAuthorsQuery : IRequest<IEnumerable<AuthorDTO>>
    {
    }
}