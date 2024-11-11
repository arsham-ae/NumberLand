using MediatR;
using NumberLand.Command.Operator.Command;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Numbers;
using NumberLand.Utility;

namespace NumberLand.Command.Operator.Handler
{
    public class CreateOperatorHandler : IRequestHandler<CreateOperatorCommand, CommandsResponse<OperatorModel>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateOperatorHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandsResponse<OperatorModel>> Handle(CreateOperatorCommand request, CancellationToken cancellationToken)
        {
            request.OperatorModel.slug = SlugHelper.GenerateSlug(request.OperatorModel.operatorCode);
            await _unitOfWork.nOperator.Add(request.OperatorModel);
            await _unitOfWork.Save();
            return new CommandsResponse<OperatorModel>
            {
                status = "Success",
                message = "Operator Created Successfully.",
                data = await _unitOfWork.nOperator.Get(o => o.id == request.OperatorModel.id)
            };
        }
    }
}
