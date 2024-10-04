using NumberLand.DataAccess.Data;
using NumberLand.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberLand.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private myDbContext _context;
        public INumberRepo number { get; private set; }

        public IOperatorRepo nOperator { get; private set; }

        public UnitOfWork(myDbContext context)
        {
            _context = context;
            number = new NumberRepo(_context);
            nOperator = new OperatorRepo(_context);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
