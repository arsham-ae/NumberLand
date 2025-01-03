using AutoMapper;
using MediatR;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Query.Blog.Query;

namespace NumberLand.Query.Blog.Handler
{
    public class GetBlogByCatSlugHandler : IRequestHandler<GetBlogByCatSlugQuery, IEnumerable<BlogDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBlogByCatSlugHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<BlogDTO>> Handle(GetBlogByCatSlugQuery request, CancellationToken cancellationToken)
        {
            var blogs = _mapper.Map<List<BlogDTO>>((await _unitOfWork.blog.GetAll(includeProp: "author, blogCategories.category"))
        .Where(b => b.blogCategories != null && b.blogCategories.Any(bc => bc.category.slug == request.CatSlug))
        .ToList());
            return blogs == null ? null : blogs;
        }
    }
}
