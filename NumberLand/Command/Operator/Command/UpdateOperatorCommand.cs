using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using NumberLand.DataAccess.DTOs;
using NumberLand.Models.Numbers;

namespace NumberLand.Command.Operator.Command
{
    public class UpdateOperatorCommand : IRequest<CommandsResponse<OperatorDTO>>
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
