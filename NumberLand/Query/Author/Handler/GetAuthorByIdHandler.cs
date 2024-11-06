using AutoMapper;
using MediatR;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Query.Author.Query;

namespace NumberLand.Query.Author.Handler
{
    public class GetAuthorByIdHandler : IRequestHandler<GetAuthorByIdQuery, AuthorDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAuthorByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AuthorDTO> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            var get = _mapper.Map<AuthorDTO>(await _unitOfWork.author.Get(a => a.id == request.Id));
            return get == null ? null : get;
        }
    }
}
