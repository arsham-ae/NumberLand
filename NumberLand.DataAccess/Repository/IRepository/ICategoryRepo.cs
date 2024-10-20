using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NumberLand.Models.Numbers;

namespace NumberLand.DataAccess.Repository.IRepository
{
    public interface ICategoryRepo : IRepo<CategoryModel>
    {
        void Update(CategoryModel category);
        void Patch(int id, [FromBody] JsonPatchDocument<CategoryModel> patchDoc);
    }
}
