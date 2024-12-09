using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NumberLand.Models.Numbers;

namespace NumberLand.DataAccess.Repository.IRepository
{
    public interface ICategoryRepo : IRepo<CategoryModel>
    {
        Task Update(CategoryModel category);
        Task Patch(int id, [FromBody] JsonPatchDocument<CategoryModel> patchDoc);
    }
}
