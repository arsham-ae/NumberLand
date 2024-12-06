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
        IBlogCategoryRepo blogCategory { get; }
        ICountryRepo country { get; }
        IApplicationRepo application { get; }

        Task Save();
    }
}
