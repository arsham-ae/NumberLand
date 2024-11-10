using AutoMapper;
using MediatR;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Query.Blog.Query;

namespace NumberLand.Query.Blog.Handler
{
    public class GetAllBlogsCategoryHandler : IRequestHandler<GetAllBlogsCategoryQuery, IEnumerable<BlogCategoryDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllBlogsCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BlogCategoryDTO>> Handle(GetAllBlogsCategoryQuery request, CancellationToken cancellationToken)
        {
            var getall = _mapper.Map<IEnumerable<BlogCategoryDTO>>(await _unitOfWork.blogCategory.GetAll());
            return getall == null ? null : getall;
        }
    }
}
