using AutoMapper;
using MediatR;
using NumberLand.Command.Operator.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;

namespace NumberLand.Command.Operator.Handler
{
    public class UpdateOperatorHandler : IRequestHandler<UpdateOperatorCommand, CommandsResponse<OperatorDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateOperatorHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CommandsResponse<OperatorDTO>> Handle(UpdateOperatorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _unitOfWork.nOperator.Patch(request.Id, request.OperatorModel);
                return new CommandsResponse<OperatorDTO>
                {
                    status = "Success",
                    message = $"Operator With Id {request.Id} Updated SuccessFully!",
                    data = _mapper.Map<OperatorDTO>(await _unitOfWork.nOperator.Get(o => o.id == request.Id))
                };
            }
            catch (Exception ex)
            {
                return new CommandsResponse<OperatorDTO>
                {
                    status = "Fail",
                    message = ex.InnerException.Message
                };
            }
        }
    }
}
