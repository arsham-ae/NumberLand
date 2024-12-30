using AutoMapper;
using MediatR;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Query.Page.Query;

namespace NumberLand.Query.Page.Handler
{
    public class GetPageCategoryBySlugHandler : IRequestHandler<GetPageCategoryBySlugQuery, PageCategoryDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPageCategoryBySlugHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<PageCategoryDTO> Handle(GetPageCategoryBySlugQuery request, CancellationToken cancellationToken)
        {
            var pageCategory = _mapper.Map<PageCategoryDTO>(await _unitOfWork.pageCategory.Get(pc => pc.slug == request.Slug));
            return pageCategory == null ? null : pageCategory;
        }
    }
}
