using MediatR;
using NumberLand.Command.Number.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;

namespace NumberLand.Command.Number.Handler
{
    public class DeleteRangeNumberHandler : IRequestHandler<DeleteRangeNumberCommand, CommandsResponse<NumberDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRangeNumberHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandsResponse<NumberDTO>> Handle(DeleteRangeNumberCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var get = (await _unitOfWork.number.GetAll()).Where(p => request.Ids.Contains(p.id)).ToList();
                if (get == null || !get.Any())
                {
                    return new CommandsResponse<NumberDTO>
                    {
                        status = "Fail",
                        message = $"Number with Id {string.Join(",", request.Ids)} Not Found!"
                    };
                }
                _unitOfWork.number.DeleteRange(get);
                await _unitOfWork.Save();
                return new CommandsResponse<NumberDTO>
                {
                    status = "Success",
                    message = $"Numbers with Id {string.Join(",", request.Ids)} Deleted Successfully!"
                };
            }
            catch (Exception ex)
            {
                return new CommandsResponse<NumberDTO>
                {
                    status = "Fail",
                    message = ex.Message
                };
            }
        }
    }
}
