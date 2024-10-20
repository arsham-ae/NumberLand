using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NumberLand.Models.Blogs;

namespace NumberLand.DataAccess.Repository.IRepository
{
    public interface IBlogRepo : IRepo<BlogModel>
    {
        void Update(BlogModel blog);
        void Patch(int id, [FromBody] JsonPatchDocument<BlogModel> patchDoc);
    }
}
