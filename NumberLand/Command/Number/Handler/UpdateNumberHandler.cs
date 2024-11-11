using AutoMapper;
using MediatR;
using NumberLand.Command.Number.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Numbers;
using NumberLand.Utility;

namespace NumberLand.Command.Number.Handler
{
    public class UpdateNumberHandler : IRequestHandler<UpdateNumberCommand, CommandsResponse<NumberDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateNumberHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<CommandsResponse<NumberDTO>> Handle(UpdateNumberCommand request, CancellationToken cancellationToken)
        {
            var mappedNum = _mapper.Map<NumberModel>(request.NumberDto);
            mappedNum.slug = SlugHelper.GenerateSlug(request.NumberDto.number);
            await _unitOfWork.number.UpdateAsync(mappedNum);
            return new CommandsResponse<NumberDTO>
            {
                status = "Success",
                message = "Number Updated SuccessFully!",
                data = _mapper.Map<NumberDTO>(mappedNum)
            };
        }
    }
}
