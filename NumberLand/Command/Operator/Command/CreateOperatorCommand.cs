using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Command.Operator.Command
{
    public class CreateOperatorCommand : IRequest<CommandsResponse<OperatorDTO>>
    {
        public CreateOperatorDTO OperatorModel { get; set; }
        public CreateOperatorCommand(CreateOperatorDTO operatorModel)
        {
            OperatorModel = operatorModel;
        }
    }
}
