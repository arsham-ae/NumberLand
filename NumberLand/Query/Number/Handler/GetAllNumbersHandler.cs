using AutoMapper;
using MediatR;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Query.Number.Query;

namespace NumberLand.Query.Number.Handler
{
    public class GetAllNumbersHandler : IRequestHandler<GetAllNumbersQuery, List<NumberDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllNumbersHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<NumberDTO>> Handle(GetAllNumbersQuery request, CancellationToken cancellationToken)
        {
            var numbers = _mapper.Map<List<NumberDTO>>(await _unitOfWork.number.GetAll(includeProp: "category,nOperator"));
            return numbers == null ? null : numbers;
        }
    }
}
