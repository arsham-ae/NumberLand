using AutoMapper;
using MediatR;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Query.Application.Query;

namespace NumberLand.Query.Application.Handler
{
    public class GetAllApplicationsHandler : IRequestHandler<GetAllApplicationsQuery, IEnumerable<ApplicationDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllApplicationsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ApplicationDTO>> Handle(GetAllApplicationsQuery request, CancellationToken cancellationToken)
        {
            var applications = _mapper.Map<IEnumerable<ApplicationDTO>>(await _unitOfWork.application.GetAll());
            return applications == null ? null : applications;
        }
    }
}
