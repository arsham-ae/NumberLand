using Azure;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using NumberLand.Models.Numbers;

namespace NumberLand.Command
{
    public class PatchNumberCommand : IRequest<string>
    {
        public int Id { get; set; }
        public JsonPatchDocument<NumberModel> PatchDoc { get; set; }
        public PatchNumberCommand(int id, JsonPatchDocument<NumberModel> patchDoc)
        {
            Id = id;
            PatchDoc = patchDoc;
        }
    }
}
