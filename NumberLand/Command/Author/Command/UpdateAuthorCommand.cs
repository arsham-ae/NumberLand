using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using NumberLand.DataAccess.DTOs;
using NumberLand.Models.Blogs;

namespace NumberLand.Command.Author.Command
{
    public class UpdateAuthorCommand : IRequest<CommandsResponse<AuthorDTO>>
    {
        public int Id { get; set; }
        public JsonPatchDocument<AuthorModel> JsonPatch { get; set; }
        public IFormFile File { get; set; }

        public UpdateAuthorCommand(int id, JsonPatchDocument<AuthorModel> jsonPatch, IFormFile file)
        {
            Id = id;
            JsonPatch = jsonPatch;
            File = file;
        }
    }
}