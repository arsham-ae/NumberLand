using AutoMapper;
using MediatR;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Numbers;
using NumberLand.Query.Application.Query;

namespace NumberLand.Query.Application.Handler
{
    public class GetApplicationByIdHandler : IRequestHandler<GetApplicationByIdQuery, ApplicationDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetApplicationByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApplicationDTO> Handle(GetApplicationByIdQuery request, CancellationToken cancellationToken)
        {
            var application = _mapper.Map<ApplicationDTO>(await _unitOfWork.application.Get(app => app.id == request.Id));
            return application == null ? null : application;
        }
    }
}
