using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using NumberLand.DataAccess.DTOs;
using NumberLand.Models.Pages;

namespace NumberLand.Command.Page.Command
{
    public class UpdatePageCategoryCommand : IRequest<CommandsResponse<PageCategoryDTO>>
    {
        public int Id { get; set; }
        public JsonPatchDocument<PageCategoryModel> PatchDoc { get; set; }
        public UpdatePageCategoryCommand(int id, JsonPatchDocument<PageCategoryModel> patchDoc)
        {
            Id = id;
            PatchDoc = patchDoc;
        }
    }
}
