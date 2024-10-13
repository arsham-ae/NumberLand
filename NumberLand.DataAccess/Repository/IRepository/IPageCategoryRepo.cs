using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NumberLand.Models.Numbers;
using NumberLand.Models.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberLand.DataAccess.Repository.IRepository
{
    public interface IPageCategoryRepo : IRepo<PageCategoryModel>
    {
        void Update(PageCategoryModel pageCategory);
        void Patch(int id, [FromBody] JsonPatchDocument<PageCategoryModel> patchDoc);
    }
}
