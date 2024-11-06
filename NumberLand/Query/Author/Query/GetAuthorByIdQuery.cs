using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Query.Author.Query
{
    public class GetAuthorByIdQuery : IRequest<AuthorDTO>
    {
        public int Id { get; }
        public GetAuthorByIdQuery(int id)
        {
            Id = id;
        }
    }
}
