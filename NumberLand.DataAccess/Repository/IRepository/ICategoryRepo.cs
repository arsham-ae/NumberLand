using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NumberLand.Models.Blogs;
using NumberLand.Models.Numbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberLand.DataAccess.Repository.IRepository
{
    public interface ICategoryRepo:IRepo<CategoryModel>
    {
        void Update(CategoryModel category);
        void Patch(int id, [FromBody] JsonPatchDocument<CategoryModel> patchDoc);
    }
}
