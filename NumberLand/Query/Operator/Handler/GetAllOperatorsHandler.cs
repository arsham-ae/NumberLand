using MediatR;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Numbers;
using NumberLand.Query.Operator.Query;

namespace NumberLand.Query.Operator.Handler
{
    public class GetAllOperatorsHandler : IRequestHandler<GetAllOperatorsQuery, IEnumerable<OperatorModel>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllOperatorsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<OperatorModel>> Handle(GetAllOperatorsQuery request, CancellationToken cancellationToken)
        {
            var operators = await _unitOfWork.nOperator.GetAll();
            return operators == null ? null : operators;
        }
    }
}
