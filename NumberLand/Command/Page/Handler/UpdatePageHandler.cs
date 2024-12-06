using AutoMapper;
using MediatR;
using NumberLand.Command.Page.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;

namespace NumberLand.Command.Page.Handler
{
    public class UpdatePageHandler : IRequestHandler<UpdatePageCommand, CommandsResponse<PageDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdatePageHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CommandsResponse<PageDTO>> Handle(UpdatePageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _unitOfWork.page.Patch(request.Id, request.PatchDoc);
                return new CommandsResponse<PageDTO>
                {
                    status = "Success",
                    message = "Page Updated Successfully",
                    data = _mapper.Map<PageDTO>(await _unitOfWork.page.Get(p => p.id == request.Id))
                };
            }
            catch (Exception ex)
            {
                return new CommandsResponse<PageDTO>
                {
                    status = "Fail",
                    message = ex.InnerException.Message
                };
            }
        }
    }
}
