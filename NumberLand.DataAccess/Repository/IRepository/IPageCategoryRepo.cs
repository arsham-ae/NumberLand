using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NumberLand.Models.Pages;

namespace NumberLand.DataAccess.Repository.IRepository
{
    public interface IPageCategoryRepo : IRepo<PageCategoryModel>
    {
        void Update(PageCategoryModel pageCategory);
        void Patch(int id, [FromBody] JsonPatchDocument<PageCategoryModel> patchDoc);
    }
}
