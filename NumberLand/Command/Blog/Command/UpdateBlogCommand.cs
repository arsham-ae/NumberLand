using Azure;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using NumberLand.DataAccess.DTOs;
using NumberLand.Models.Blogs;

namespace NumberLand.Command.Blog.Command
{
    public class UpdateBlogCommand : IRequest<CommandsResponse<BlogDTO>>
    {
        public int Id { get; set; }
        public JsonPatchDocument<BlogModel> PatchDoc { get; set; }
        public IFormFile File { get; set; }
        public UpdateBlogCommand(int id, JsonPatchDocument<BlogModel> patchDoc, IFormFile file)
        {
            Id = id;
            PatchDoc = patchDoc;
            File = file;
        }
    }
}
