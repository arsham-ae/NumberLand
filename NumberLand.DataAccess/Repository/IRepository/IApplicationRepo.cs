using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NumberLand.Models.Numbers;

namespace NumberLand.DataAccess.Repository.IRepository
{
    public interface IApplicationRepo : IRepo<ApplicationModel>
    {
        Task Update(ApplicationModel category);
        Task Patch(int id, [FromBody] JsonPatchDocument<ApplicationModel> patchDoc);
    }
}
