using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using NumberLand.DataAccess.DTOs;
using NumberLand.Models.Numbers;

namespace NumberLand.Command.Number.Command
{
    public class UpdateNumberCommand : IRequest<CommandsResponse<NumberDTO>>
    {
        public int Id { get; set; }
        public JsonPatchDocument<NumberModel> PatchDoc { get; set; }
        public UpdateNumberCommand(int id, JsonPatchDocument<NumberModel> patchDoc)
        {
            Id = id;
            PatchDoc = patchDoc;
        }
    }
}
