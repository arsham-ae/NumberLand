using AutoMapper;
using MediatR;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Query.Blog.Query;

namespace NumberLand.Query.Blog.Handler
{
    public class GetAllBlogsHandler : IRequestHandler<GetAllBlogsQuery, IEnumerable<BlogDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllBlogsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BlogDTO>> Handle(GetAllBlogsQuery request, CancellationToken cancellationToken)
        {
            var getall = _mapper.Map<IEnumerable<BlogDTO>>(await _unitOfWork.blog.GetAll(includeProp: "author, blogCategories.category"));
            return getall == null ? null : getall;
        }
    }
}
