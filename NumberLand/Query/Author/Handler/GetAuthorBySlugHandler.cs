using AutoMapper;
using MediatR;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Query.Author.Query;

namespace NumberLand.Query.Author.Handler
{
    public class GetAuthorBySlugHandler : IRequestHandler<GetAuthorBySlugQuery, AuthorDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAuthorBySlugHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthorDTO> Handle(GetAuthorBySlugQuery request, CancellationToken cancellationToken)
        {
            var author = _mapper.Map<AuthorDTO>(await _unitOfWork.author.Get(a => a.slug == request.Slug));
            return author == null ? null : author;
        }
    }
}
