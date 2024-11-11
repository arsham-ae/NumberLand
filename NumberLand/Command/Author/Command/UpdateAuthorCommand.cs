using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using NumberLand.DataAccess.DTOs;
using NumberLand.Models.Blogs;

namespace NumberLand.Command.Author.Command
{
    public class UpdateAuthorCommand : IRequest<CommandsResponse<AuthorDTO>>
    {
        public int Id { get; set; }
        public JsonPatchDocument<AuthorModel> JsonPatch { get; }
        public IFormFile File { get; }

        public UpdateAuthorCommand(int id, JsonPatchDocument<AuthorModel> jsonPatch, IFormFile file)
        {
            Id = id;
            JsonPatch = jsonPatch;
            File = file;
        }
    }
}