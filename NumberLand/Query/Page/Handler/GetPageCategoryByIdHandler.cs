using AutoMapper;
using MediatR;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Query.Page.Query;

namespace NumberLand.Query.Page.Handler
{
    public class GetPageCategoryByIdHandler : IRequestHandler<GetPageCategoryByIdQuery, PageCategoryDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetPageCategoryByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PageCategoryDTO> Handle(GetPageCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var get = _mapper.Map<PageCategoryDTO>(await _unitOfWork.pageCategory.Get(o => o.id == request.Id, includeProp: "parentCategory"));
            return get == null ? null : get;
        }
    }
}
