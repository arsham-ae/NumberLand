using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NumberLand.Models.Blogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberLand.DataAccess.Repository.IRepository
{
    public interface IBlogRepo : IRepo<BlogModel>
    {
        void Update(BlogModel blog);
        void Patch(int id, [FromBody] JsonPatchDocument<BlogModel> patchDoc);
    }
}
