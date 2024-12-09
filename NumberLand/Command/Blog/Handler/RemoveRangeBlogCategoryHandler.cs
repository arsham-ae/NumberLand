using MediatR;
using NumberLand.Command.Blog.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;

namespace NumberLand.Command.Blog.Handler
{
    public class RemoveRangeBlogCategoryHandler : IRequestHandler<RemoveRangeBlogCategoryCommand, CommandsResponse<BlogCategoryDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveRangeBlogCategoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandsResponse<BlogCategoryDTO>> Handle(RemoveRangeBlogCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var blogCat = (await _unitOfWork.blogCategory.GetAll()).Where(p => request.Ids.Contains(p.id)).ToList();
                if (!blogCat.Any())
                {
                    return new CommandsResponse<BlogCategoryDTO>
                    {
                        status = "Fail",
                        message = $"BlogCategory With Id {string.Join(",", request.Ids)} Not Found!"
                    };
                }
                _unitOfWork.blogCategory.DeleteRange(blogCat);
                await _unitOfWork.Save();

                return new CommandsResponse<BlogCategoryDTO>
                {
                    status = "Success",
                    message = $"BlogCategory With Id {string.Join(",", request.Ids)} Deleted Successfully."
                };
            }
            catch (Exception ex)
            {
                return new CommandsResponse<BlogCategoryDTO>
                {
                    status = "Fail",
                    message = ex.InnerException.Message
                };
            }
        }
    }
}
