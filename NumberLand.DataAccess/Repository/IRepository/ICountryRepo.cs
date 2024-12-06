using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NumberLand.Models.Numbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberLand.DataAccess.Repository.IRepository
{
    public interface ICountryRepo : IRepo<CountryModel>
    {
        Task Update(CountryModel category);
        Task Patch(int id, [FromBody] JsonPatchDocument<CountryModel> patchDoc);
    }
}
