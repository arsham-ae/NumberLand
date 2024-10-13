using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NumberLand.Models.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberLand.DataAccess.Repository.IRepository
{
    public interface IPageRepo : IRepo<PageeModel>
    {
        void Update(PageeModel page);
        void Patch(int id, [FromBody] JsonPatchDocument<PageeModel> patchDoc);
    }
}
