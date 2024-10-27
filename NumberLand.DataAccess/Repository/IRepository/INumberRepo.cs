using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NumberLand.Models.Numbers;

namespace NumberLand.DataAccess.Repository.IRepository
{
    public interface INumberRepo : IRepo<NumberModel>
    {
        Task Update(NumberModel upNumber);
        Task Patch(int id, [FromBody] JsonPatchDocument<NumberModel> patchDoc);
    }
}
