using AutoMapper;
using MediatR;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Query.Blog.Query;

namespace NumberLand.Query.Blog.Handler
{
    public class GetBlogCatBySlugHandler : IRequestHandler<GetBlogCategoryBySlugQuery, BlogCategoryDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBlogCatBySlugHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<BlogCategoryDTO> Handle(GetBlogCategoryBySlugQuery request, CancellationToken cancellationToken)
        {
            var blogCategory = _mapper.Map<BlogCategoryDTO>(await _unitOfWork.blogCategory.Get(bc => bc.slug == request.Slug));
            return blogCategory == null ? null : blogCategory;
        }
    }
}
