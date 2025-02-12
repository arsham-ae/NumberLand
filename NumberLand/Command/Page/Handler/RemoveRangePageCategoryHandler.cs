﻿using MediatR;
using NumberLand.Command.Page.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;

namespace NumberLand.Command.Page.Handler
{
    public class RemoveRangePageCategoryHandler : IRequestHandler<RemoveRangePageCategoryCommand, CommandsResponse<PageCategoryDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveRangePageCategoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandsResponse<PageCategoryDTO>> Handle(RemoveRangePageCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var pageCats = (await _unitOfWork.pageCategory.GetAll()).Where(p => request.Ids.Contains(p.id)).ToList();
                if (pageCats == null || !pageCats.Any())
                {
                    return new CommandsResponse<PageCategoryDTO>
                    {
                        status = "Fail",
                        message = $"PageCategories with Id {string.Join(",", request.Ids)} Not Found!"
                    };
                }
                _unitOfWork.pageCategory.DeleteRange(pageCats);
                await _unitOfWork.Save();
                return new CommandsResponse<PageCategoryDTO>
                {
                    status = "Success",
                    message = $"PageCategories with Id {string.Join(",", request.Ids)} Deleted Successfully!"
                };
            }
            catch (Exception ex)
            {
                return new CommandsResponse<PageCategoryDTO>
                {
                    status = "Fail",
                    message = ex.InnerException.Message
                };
            }
        }
    }
}
