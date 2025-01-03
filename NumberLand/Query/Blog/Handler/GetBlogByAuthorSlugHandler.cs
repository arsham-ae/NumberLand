using AutoMapper;
using MediatR;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Query.Blog.Query;

namespace NumberLand.Query.Blog.Handler
{
    public class GetBlogByAuthorSlugHandler : IRequestHandler<GetBlogByAuthorSlugQuery, IEnumerable<BlogDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBlogByAuthorSlugHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<BlogDTO>> Handle(GetBlogByAuthorSlugQuery request, CancellationToken cancellationToken)
        {
            var blogs = _mapper.Map<List<BlogDTO>>((await _unitOfWork.blog.GetAll(includeProp: "author, blogCategories.category"))
        .Where(b => b.author != null && b.author.slug == request.AuthorSlug)
        .ToList());
            return blogs == null ? null : blogs;
        }
    }
}
