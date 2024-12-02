using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using NumberLand.Models.Blogs;

namespace NumberLand.DataAccess.DTOs
{
    public class AuthorDTO
    {
        public int authorId { get; set; }
        public string authorSlug { get; set; }
        public string authorName { get; set; }
        public string authorDescription { get; set; }
        public string imagePath { get; set; }
    }
    public class CreateAuthorDTO
    {
        public int authorId { get; set; }
        public string authorSlug { get; set; }
        public string authorName { get; set; }
        public string authorDescription { get; set; }
    }
    public class UpdateAuthorDTO
    {
        public JsonPatchDocument<AuthorModel>? PatchDoc { get; set; }
        public IFormFile? ImageFile { get; set; }
    }

}