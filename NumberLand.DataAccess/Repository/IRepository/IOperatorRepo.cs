using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NumberLand.Models.Numbers;

namespace NumberLand.DataAccess.Repository.IRepository
{
    public interface IOperatorRepo : IRepo<OperatorModel>
    {
        void Update(OperatorModel upOperator);
        void Patch(int id, [FromBody] JsonPatchDocument<OperatorModel> patchDoc);
    }
}
