using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using NumberLand.DataAccess.DTOs;
using NumberLand.Models.Numbers;

namespace NumberLand.Command.Country.Command
{
    public class UpdateCountryCommand : IRequest<CommandsResponse<CountryDTO>>
    {
        public int Id { get; set; }
        public JsonPatchDocument<CountryModel>? PatchDoc { get; set; }
        public IFormFile File { get; set; }
        public UpdateCountryCommand(int id, JsonPatchDocument<CountryModel>? patchDoc, IFormFile file)
        {
            Id = id;
            PatchDoc = patchDoc;
            File = file;
        }
    }
}
