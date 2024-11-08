using AutoMapper;
using MediatR;
using NumberLand.Command.Number.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;

namespace NumberLand.Command.Number.Handler
{
    public class PatchNumberHandler : IRequestHandler<PatchNumberCommand, CommandsResponse<NumberDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PatchNumberHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CommandsResponse<NumberDTO>> Handle(PatchNumberCommand request, CancellationToken cancellationToken)
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
    }
}
