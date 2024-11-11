using Azure;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NumberLand.DataAccess.DTOs;
using NumberLand.Models.Pages;

namespace NumberLand.Command.Page.Command
{
    public class UpdatePageCommand : IRequest<CommandsResponse<PageDTO>>
    {
        public int Id { get; set; }
        public JsonPatchDocument<PageeModel> PatchDoc { get; set; }
        public UpdatePageCommand(int id, JsonPatchDocument<PageeModel> patchDoc)
        {
            Id = id;
            PatchDoc = patchDoc;
        }
    }
}
