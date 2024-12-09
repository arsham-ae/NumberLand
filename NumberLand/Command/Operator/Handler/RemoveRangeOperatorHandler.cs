using MediatR;
using NumberLand.Command.Operator.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;

namespace NumberLand.Command.Operator.Handler
{
    public class RemoveRangeOperatorHandler : IRequestHandler<RemoveRangeOperatorCommand, CommandsResponse<OperatorDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveRangeOperatorHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandsResponse<OperatorDTO>> Handle(RemoveRangeOperatorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var operators = (await _unitOfWork.nOperator.GetAll()).Where(p => request.Ids.Contains(p.id)).ToList();
                if (operators == null || !operators.Any())
                {
                    return new CommandsResponse<OperatorDTO>
                    {
                        status = "Fail",
                        message = $"Operators with Id {string.Join(",", request.Ids)} Not Found!"
                    };
                }
                _unitOfWork.nOperator.DeleteRange(operators);
                await _unitOfWork.Save();
                return new CommandsResponse<OperatorDTO>
                {
                    status = "Success",
                    message = $"Operators with Id {string.Join(",", request.Ids)} Deleted Successfully!"
                };
            }
            catch (Exception ex)
            {
                return new CommandsResponse<OperatorDTO>
                {
                    status = "Fail",
                    message = ex.InnerException.Message
                };
            }
        }
    }
}