using AutoMapper;
using MediatR;
using NumberLand.Command.Number.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Numbers;
using NumberLand.Utility;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NumberLand.Command.Number.Handler
{
    public class CreateNumberHandler : IRequestHandler<CreateNumberCommand, CommandsResponse<NumberDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateNumberHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandsResponse<NumberDTO>> Handle(CreateNumberCommand request, CancellationToken cancellationToken)
        {
            var mappedNum = _mapper.Map<NumberModel>(request.NumberDTO);
            mappedNum.slug = SlugHelper.GenerateSlug(request.NumberDTO.number);
            await _unitOfWork.number.Add(mappedNum);
            await _unitOfWork.Save();
            return new CommandsResponse<NumberDTO>
            {
                status = "Success",
                message = $"{mappedNum.number} With Id {mappedNum.id} Created Successfully.",
                data = _mapper.Map<NumberDTO>(mappedNum)
            };
        }
    }
}
