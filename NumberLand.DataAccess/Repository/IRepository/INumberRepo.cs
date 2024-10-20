using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NumberLand.Models.Numbers;

namespace NumberLand.DataAccess.Repository.IRepository
{
    public interface INumberRepo : IRepo<NumberModel>
    {
        void Update(NumberModel upNumber);
        void Patch(int id, [FromBody] JsonPatchDocument<NumberModel> patchDoc);
    }
}
