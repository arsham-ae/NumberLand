using AutoMapper;
using MediatR;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Numbers;
using NumberLand.Query.Operator.Query;

namespace NumberLand.Query.Operator.Handler
{
    public class GetOperatorByIdHandler : IRequestHandler<GetOperatorByIdQuery, OperatorDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetOperatorByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<OperatorDTO> Handle(GetOperatorByIdQuery request, CancellationToken cancellationToken)
        {
            var get = _mapper.Map<OperatorDTO>(await _unitOfWork.nOperator.Get(o => o.id == request.Id));
            return get == null ? null : get;
        }
    }
}
