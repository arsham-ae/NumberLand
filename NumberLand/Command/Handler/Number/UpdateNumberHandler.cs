using AutoMapper;
using MediatR;
using NumberLand.Command.Number;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Numbers;
using NumberLand.Utility;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NumberLand.Command.Handler.Number
{
    public class UpdateNumberHandler : IRequestHandler<UpdateNumberCommand, UpdateNumberResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateNumberHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<UpdateNumberResponse> Handle(UpdateNumberCommand request, CancellationToken cancellationToken)
        {
            var mappedNum = _mapper.Map<NumberModel>(request);
            mappedNum.slug = SlugHelper.GenerateSlug(request.number);
            _unitOfWork.number.Update(mappedNum);
            return new UpdateNumberResponse
            {
                Id = mappedNum.id,
                message = "Number Updated SuccessFully!"
            };
        }
    }
}
