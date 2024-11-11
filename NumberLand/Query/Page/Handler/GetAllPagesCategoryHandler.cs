using AutoMapper;
using MediatR;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Query.Page.Query;

namespace NumberLand.Query.Page.Handler
{
    public class GetAllPagesCategoryHandler : IRequestHandler<GetAllPagesCategoryQuery, IEnumerable<PageCategoryDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllPagesCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PageCategoryDTO>> Handle(GetAllPagesCategoryQuery request, CancellationToken cancellationToken)
        {
            var getall = _mapper.Map<List<PageCategoryDTO>>(await _unitOfWork.pageCategory.GetAll(includeProp: "parentCategory"));
            return getall == null ? null : getall;
        }
    }
}
