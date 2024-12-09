using AutoMapper;
using MediatR;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Query.Operator.Query;

namespace NumberLand.Query.Operator.Handler
{
    public class GetAllOperatorsHandler : IRequestHandler<GetAllOperatorsQuery, IEnumerable<OperatorDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllOperatorsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OperatorDTO>> Handle(GetAllOperatorsQuery request, CancellationToken cancellationToken)
        {
            var operators = _mapper.Map<List<OperatorDTO>>(await _unitOfWork.nOperator.GetAll());
            return operators == null ? null : operators;
        }
    }
}
