﻿using NumberLand.DataAccess.Data;
using NumberLand.DataAccess.Repository.IRepository;

namespace NumberLand.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly myDbContext _context;
        public INumberRepo number { get; private set; }

        public IOperatorRepo nOperator { get; private set; }

        public IAuthorRepo author { get; private set; }

        public IBlogRepo blog { get; private set; }

        public ICategoryRepo category { get; private set; }

        public IPageCategoryRepo pageCategory { get; private set; }

        public IPageRepo page { get; private set; }
        public IBlogCategoryRepo blogCategory { get; private set; }
        public ICountryRepo country { get; private set; }
        public IApplicationRepo application { get; private set; }

        public UnitOfWork(myDbContext context)
        {
            _context = context;
            number = new NumberRepo(_context);
            nOperator = new OperatorRepo(_context);
            author = new AuthorRepo(_context);
            blog = new BlogRepo(_context);
            category = new CategoryRepo(_context);
            pageCategory = new PageCategoryRepo(_context);
            page = new PageRepo(_context);
            blogCategory = new BlogCategoryRepo(_context);
            country = new CountryRepo(_context);
            application = new ApplicationRepo(_context);
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
