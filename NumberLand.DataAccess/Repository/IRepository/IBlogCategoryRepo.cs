using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NumberLand.Models.Blogs;

namespace NumberLand.DataAccess.Repository.IRepository
{
    public interface IBlogCategoryRepo : IRepo<BlogCategoryModel>
    {
        Task Update(BlogCategoryModel blog);
        Task Patch(int id, [FromBody] JsonPatchDocument<BlogCategoryModel> patchDoc);
    }
}
