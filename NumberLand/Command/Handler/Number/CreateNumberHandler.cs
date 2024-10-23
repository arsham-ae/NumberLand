using AutoMapper;
using MediatR;
using NumberLand.Command.Number;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Numbers;
using NumberLand.Utility;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NumberLand.Command.Handler.Number
{
    public class CreateNumberHandler : IRequestHandler<CreateNumberCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateNumberHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(CreateNumberCommand request, CancellationToken cancellationToken)
        {
            var mappedNum = _mapper.Map<NumberModel>(request);
            mappedNum.slug = SlugHelper.GenerateSlug(request.number);
            _unitOfWork.number.Add(mappedNum);
            _unitOfWork.Save();
            return $"Number {mappedNum.number} With Id {mappedNum.id} Created SuccessFully!";
        }
    }
}
