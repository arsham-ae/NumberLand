using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Helper;
using NumberLand.Models.Blogs;

namespace NumberLand.DataAccess.Repository.IRepository
{
    public interface IBlogRepo : IRepo<BlogModel>
    {
        Task<IEnumerable<BlogModel>> GetAllBlogs(QueryObject query, string? includeProp = null);
        Task Update(BlogModel blog);
        Task Patch(int id, [FromBody] JsonPatchDocument<BlogModel> patchDoc);
    }
}
