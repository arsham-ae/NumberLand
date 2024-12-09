using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NumberLand.Models.Blogs;

namespace NumberLand.DataAccess.Repository.IRepository
{
    public interface IBlogRepo : IRepo<BlogModel>
    {
        Task Update(BlogModel blog);
        Task Patch(int id, [FromBody] JsonPatchDocument<BlogModel> patchDoc);
    }
}
