using AutoMapper;
using MediatR;
using NumberLand.Command.Number.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;

namespace NumberLand.Command.Number.Handler
{
    public class UpdateNumberHandler : IRequestHandler<UpdateNumberCommand, CommandsResponse<NumberDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateNumberHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CommandsResponse<NumberDTO>> Handle(UpdateNumberCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var getNumber = await _unitOfWork.number.Get(n => n.id == request.Id);
                await _unitOfWork.number.PatchAsync(request.Id, request.PatchDoc);
                return new CommandsResponse<NumberDTO>
                {
                    status = "Success",
                    message = $"Number With Id {request.Id} Updated SuccessFully!",
                    data = _mapper.Map<NumberDTO>(getNumber)
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
