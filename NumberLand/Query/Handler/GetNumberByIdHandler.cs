using AutoMapper;
using MediatR;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Numbers;

namespace NumberLand.Query.Handler
{
    public class GetNumberByIdHandler : IRequestHandler<GetNumberByIdQuery, NumberDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetNumberByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<NumberDTO> Handle(GetNumberByIdQuery request, CancellationToken cancellationToken)
        {
            var number = _mapper.Map<NumberDTO>(await _unitOfWork.number.Get(n => n.id == request.Id, includeProp: "category,nOperator"));

            return number == null ? null : number;
        }
    }
}
