using AutoMapper;
using MediatR;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Query.Application.Query;

namespace NumberLand.Query.Application.Handler
{
    public class GetApplicationBySlugHandler : IRequestHandler<GetApplicationBySlugQuery, ApplicationDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetApplicationBySlugHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApplicationDTO> Handle(GetApplicationBySlugQuery request, CancellationToken cancellationToken)
        {
            var application = _mapper.Map<ApplicationDTO>(await _unitOfWork.application.Get(a => a.slug == request.Slug));
            return application == null ? null : application;
        }
    }
}
