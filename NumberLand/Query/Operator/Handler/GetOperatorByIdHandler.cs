using AutoMapper;
using MediatR;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Numbers;
using NumberLand.Query.Operator.Query;

namespace NumberLand.Query.Operator.Handler
{
    public class GetOperatorByIdHandler : IRequestHandler<GetOperatorByIdQuery, OperatorModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetOperatorByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<OperatorModel> Handle(GetOperatorByIdQuery request, CancellationToken cancellationToken)
        {
            var get = await _unitOfWork.nOperator.Get(o => o.id == request.Id);
            return get == null ? null : get;
        }
    }
}
