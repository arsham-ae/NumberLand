using MediatR;
using NumberLand.Command.Operator.Command;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Numbers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NumberLand.Command.Operator.Handler
{
    public class RemoveOperatorHandler : IRequestHandler<RemoveOperatorCommand, CommandsResponse<OperatorModel>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveOperatorHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandsResponse<OperatorModel>> Handle(RemoveOperatorCommand request, CancellationToken cancellationToken)
        {
            var get = await _unitOfWork.nOperator.Get(o => o.id == request.Id);
            if (get == null)
            {
                return new CommandsResponse<OperatorModel>
                {
                    status = "Fail",
                    message = $"Operator with Id {request.Id} Not Found!"
                };
            }
            _unitOfWork.nOperator.Delete(get);
            await _unitOfWork.Save();
            return new CommandsResponse<OperatorModel>
            {
                status = "Success",
                message = $"Operator {get.operatorCode} with Id {request.Id} Deleted Successfully!"
            };
        }
    }
}
