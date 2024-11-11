using MediatR;
using NumberLand.Command.Operator.Command;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Numbers;

namespace NumberLand.Command.Operator.Handler
{
    public class RemoveRangeOperatorHandler : IRequestHandler<RemoveRangeOperatorCommand, CommandsResponse<OperatorModel>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveRangeOperatorHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandsResponse<OperatorModel>> Handle(RemoveRangeOperatorCommand request, CancellationToken cancellationToken)
        {
            var get = (await _unitOfWork.nOperator.GetAll()).Where(p => request.Ids.Contains(p.id)).ToList();
            if (get == null || !get.Any())
            {
                return new CommandsResponse<OperatorModel>
                {
                    status = "Fail",
                    message = $"Operators with Id {string.Join(",", request.Ids)} Not Found!"
                };
            }
            _unitOfWork.nOperator.DeleteRange(get);
            await _unitOfWork.Save();
            return new CommandsResponse<OperatorModel>
            {
                status = "Success",
                message = $"Operators with Id {string.Join(",", request.Ids)} Deleted Successfully!"
            };
        }
    }
}
