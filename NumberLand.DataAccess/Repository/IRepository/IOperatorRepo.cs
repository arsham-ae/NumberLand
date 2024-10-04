using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NumberLand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberLand.DataAccess.Repository.IRepository
{
    public interface IOperatorRepo : IRepo<OperatorModel>
    {
        void Update(OperatorModel upOperator);
        void Patch(int id, [FromBody] JsonPatchDocument<OperatorModel> patchDoc);
    }
}
