using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberLand.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        INumberRepo number { get; }
        IOperatorRepo nOperator { get; }
        IAuthorRepo author { get; }
        IBlogRepo blog { get; }
        ICategoryRepo category { get; }
        IPageCategoryRepo pageCategory { get; }
        IPageRepo page { get; }

        void Save();
    }
}
