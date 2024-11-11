using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using NumberLand.Models.Numbers;

namespace NumberLand.Command.Operator.Command
{
    public class UpdateOperatorCommand : IRequest<CommandsResponse<OperatorModel>>
    {
        public int Id { get; set; }
        public JsonPatchDocument<OperatorModel> OperatorModel { get; set; }
        public UpdateOperatorCommand(int id, JsonPatchDocument<OperatorModel> operatorModel)
        {
            Id = id;
            OperatorModel = operatorModel;
        }
    }
}
