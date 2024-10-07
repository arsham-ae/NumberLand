using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NumberLand.DataAccess.DTOs;
using NumberLand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberLand.DataAccess.Repository.IRepository
{
    public interface INumberRepo : IRepo<NumberModel>
    {
        void Update(NumberModel upNumber);
        void Patch(int id, [FromBody] JsonPatchDocument<NumberModel> patchDoc);
    }
}
