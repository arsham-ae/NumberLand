using AutoMapper;
using MediatR;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Query.Page.Query;

namespace NumberLand.Query.Page.Handler
{
    public class GetPageByIdHandler : IRequestHandler<GetPageByIdQuery, PageDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetPageByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PageDTO> Handle(GetPageByIdQuery request, CancellationToken cancellationToken)
        {
            var get = _mapper.Map<PageDTO>(await _unitOfWork.page.Get(o => o.id == request.Id, includeProp: "category"));
            return get == null ? null : get;
        }
    }
}
