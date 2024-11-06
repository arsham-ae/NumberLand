using AutoMapper;
using MediatR;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Query.Author.Query;

namespace NumberLand.Query.Author.Handler
{
    public class GetAllAuthorHandler : IRequestHandler<GetAllAuthorsQuery, IEnumerable<AuthorDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllAuthorHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<AuthorDTO>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
        {
            var getall = _mapper.Map<IEnumerable<AuthorDTO>>(await _unitOfWork.author.GetAll());
            return getall == null ? null : getall;
        }
    }
}
