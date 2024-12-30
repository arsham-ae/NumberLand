using AutoMapper;
using MediatR;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Query.Operator.Query;

namespace NumberLand.Query.Operator.Handler
{
    public class GetOperatorBySlugHandler : IRequestHandler<GetOperatorBySlugQuery, OperatorDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetOperatorBySlugHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<OperatorDTO> Handle(GetOperatorBySlugQuery request, CancellationToken cancellationToken)
        {
            var @operator = _mapper.Map<OperatorDTO>(await _unitOfWork.nOperator.Get(op => op.slug == request.Slug));
            return @operator == null ? null : @operator;
        }
    }
}
