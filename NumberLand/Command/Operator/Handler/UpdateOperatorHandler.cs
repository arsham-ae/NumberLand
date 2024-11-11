using MediatR;
using NumberLand.Command.Operator.Command;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Numbers;

namespace NumberLand.Command.Operator.Handler
{
    public class UpdateOperatorHandler : IRequestHandler<UpdateOperatorCommand, CommandsResponse<OperatorModel>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateOperatorHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandsResponse<OperatorModel>> Handle(UpdateOperatorCommand request, CancellationToken cancellationToken)
        {
            _unitOfWork.nOperator.Patch(request.Id, request.OperatorModel);
            return new CommandsResponse<OperatorModel>
            {
                status = "Success",
                message = "Operator Edited Successfully.",
                data = await _unitOfWork.nOperator.Get(o => o.id == request.Id)
            };
        }
    }
}
