using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using NumberLand.DataAccess.DTOs;
using NumberLand.Models.Numbers;

namespace NumberLand.Command.Application.Command
{
    public class UpdateApplicationCommand : IRequest<CommandsResponse<ApplicationDTO>>
    {
        public int Id { get; set; }
        public JsonPatchDocument<ApplicationModel> PatchDoc { get; set; }
        public IFormFile File { get; set; }
        public UpdateApplicationCommand(int id, JsonPatchDocument<ApplicationModel> patchDoc, IFormFile file)
        {
            Id = id;
            PatchDoc = patchDoc;
            File = file;
        }
    }
}
