using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NumberLand.Models.Blogs;

namespace NumberLand.DataAccess.Repository.IRepository
{
    public interface IBlogCategoryRepo : IRepo<BlogCategoryModel>
    {
        void Update(BlogCategoryModel blog);
        void Patch(int id, [FromBody] JsonPatchDocument<BlogCategoryModel> patchDoc);
    }
}
