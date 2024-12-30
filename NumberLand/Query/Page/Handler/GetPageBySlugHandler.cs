using AutoMapper;
using MediatR;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Query.Page.Query;

namespace NumberLand.Query.Page.Handler
{
    public class GetPageBySlugHandler : IRequestHandler<GetPageBySlugQuery, PageDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPageBySlugHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PageDTO> Handle(GetPageBySlugQuery request, CancellationToken cancellationToken)
        {
            var page = _mapper.Map<PageDTO>(await _unitOfWork.page.Get(p => p.slug == request.Slug));
            return page == null ? null : page;
        }
    }
}
