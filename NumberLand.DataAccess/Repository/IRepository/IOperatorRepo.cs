using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NumberLand.Models.Numbers;

namespace NumberLand.DataAccess.Repository.IRepository
{
    public interface IOperatorRepo : IRepo<OperatorModel>
    {
        Task Update(OperatorModel upOperator);
        Task Patch(int id, [FromBody] JsonPatchDocument<OperatorModel> patchDoc);
    }
}
