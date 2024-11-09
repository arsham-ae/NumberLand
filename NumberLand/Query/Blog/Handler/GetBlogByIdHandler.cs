using AutoMapper;
using MediatR;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Query.Blog.Query;

namespace NumberLand.Query.Blog.Handler
{
    public class GetBlogByIdHandler : IRequestHandler<GetBlogByIdQuery, BlogDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBlogByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BlogDTO> Handle(GetBlogByIdQuery request, CancellationToken cancellationToken)
        {
            var get = _mapper.Map<BlogDTO>(await _unitOfWork.blog.Get(o => o.id == request.Id, includeProp: "author, blogCategories.category"));
            return get == null ? null : get;
        }
    }
}
