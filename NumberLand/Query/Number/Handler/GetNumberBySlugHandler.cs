using AutoMapper;
using MediatR;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Query.Number.Query;

namespace NumberLand.Query.Number.Handler
{
    public class GetNumberBySlugHandler : IRequestHandler<GetNumberBySlugQuery, NumberDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetNumberBySlugHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<NumberDTO> Handle(GetNumberBySlugQuery request, CancellationToken cancellationToken)
        {
            var number = _mapper.Map<NumberDTO>(await _unitOfWork.number.Get(n => n.slug == request.Slug));
            return number == null ? null : number;
        }
    }
}
