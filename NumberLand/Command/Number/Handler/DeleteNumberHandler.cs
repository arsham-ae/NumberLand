using MediatR;
using NumberLand.Command.Number.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;

namespace NumberLand.Command.Number.Handler
{
    public class DeleteNumberHandler : IRequestHandler<DeleteNumberCommand, CommandsResponse<NumberDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteNumberHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandsResponse<NumberDTO>> Handle(DeleteNumberCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var number = await _unitOfWork.number.Get(o => o.id == request.id);
                if (number == null)
                {
                    return new CommandsResponse<NumberDTO>
                    {
                        status = "Fail",
                        message = $"Number with Id {request.id} Not Found!"
                    };
                }
                _unitOfWork.number.Delete(number);
                await _unitOfWork.Save();
                return new CommandsResponse<NumberDTO>
                {
                    status = "Success",
                    message = $"number {number.number} wiht Id {request.id} Deleted Successfully!"
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
