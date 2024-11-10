using AutoMapper;
using MediatR;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Query.Blog.Query;

namespace NumberLand.Query.Blog.Handler
{
    public class GetBlogCategoryByIdHandler : IRequestHandler<GetBlogCategoryByIdQuery, BlogCategoryDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBlogCategoryByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BlogCategoryDTO> Handle(GetBlogCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var get = _mapper.Map<BlogCategoryDTO>(await _unitOfWork.blogCategory.Get(o => o.id == request.Id));
            return get == null ? null : get;
        }
    }
}
