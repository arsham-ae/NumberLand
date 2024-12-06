using AutoMapper;
using MediatR;
using NumberLand.Command.Page.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;

namespace NumberLand.Command.Page.Handler
{
    public class UpdatePageCategoryHandler : IRequestHandler<UpdatePageCategoryCommand, CommandsResponse<PageCategoryDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdatePageCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CommandsResponse<PageCategoryDTO>> Handle(UpdatePageCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _unitOfWork.pageCategory.Patch(request.Id, request.PatchDoc);
                return new CommandsResponse<PageCategoryDTO>
                {
                    status = "Success",
                    message = "PageCategory Updated Successfully",
                    data = _mapper.Map<PageCategoryDTO>(await _unitOfWork.pageCategory.Get(pc => pc.id == request.Id))
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
