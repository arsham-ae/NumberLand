using AutoMapper;
using MediatR;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Query.Blog.Query;

namespace NumberLand.Query.Blog.Handler
{
    public class GetBlogBySlugHandler : IRequestHandler<GetBlogBySlugQuery, BlogDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBlogBySlugHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<BlogDTO> Handle(GetBlogBySlugQuery request, CancellationToken cancellationToken)
        {
            var blog = _mapper.Map<BlogDTO>(await _unitOfWork.blog.Get(b => b.slug == request.Slug, includeProp: "author, blogCategories.category"));
            return blog == null ? null : blog;
        }
    }
}
