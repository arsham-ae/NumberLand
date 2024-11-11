using MediatR;
using NumberLand.Models.Numbers;

namespace NumberLand.Command.Operator.Command
{
    public class CreateOperatorCommand : IRequest<CommandsResponse<OperatorModel>>
    {
        public OperatorModel OperatorModel { get; set; }
        public CreateOperatorCommand(OperatorModel operatorModel)
        {
            OperatorModel = operatorModel;
        }
    }
}
