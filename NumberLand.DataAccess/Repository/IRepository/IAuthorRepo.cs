using NumberLand.Models.Blogs;

namespace NumberLand.DataAccess.Repository.IRepository
{
    public interface IAuthorRepo : IRepo<AuthorModel>
    {
        Task Update(AuthorModel author);
    }
}
