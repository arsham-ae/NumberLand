using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NumberLand.Models.Pages;

namespace NumberLand.DataAccess.Repository.IRepository
{
    public interface IPageRepo : IRepo<PageeModel>
    {
        Task Update(PageeModel page);
        Task Patch(int id, [FromBody] JsonPatchDocument<PageeModel> patchDoc);
    }
}
