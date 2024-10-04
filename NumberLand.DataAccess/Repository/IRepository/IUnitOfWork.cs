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
        void Save();
    }
}
