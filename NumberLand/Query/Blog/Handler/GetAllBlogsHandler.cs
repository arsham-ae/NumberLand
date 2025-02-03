using AutoMapper;
using MediatR;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Query.Blog.Query;

namespace NumberLand.Query.Blog.Handler
{
    public class GetAllBlogsHandler : IRequestHandler<GetAllBlogsQuery, PaginatedResponse<BlogDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllBlogsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginatedResponse<BlogDTO>> Handle(GetAllBlogsQuery request, CancellationToken cancellationToken)
        {
            var queryObject = request.Query;

            var query = (await _unitOfWork.blog.GetAll(includeProp: "author,blogCategories.category")).AsQueryable();

            var totalItems = query.Count();

            var paginatedBlogs = query
           .Skip((queryObject.pageNumber - 1) * queryObject.limit)
           .Take(queryObject.limit)
           .ToList();

            var blogDtos = _mapper.Map<List<BlogDTO>>(paginatedBlogs);

            return new PaginatedResponse<BlogDTO>(blogDtos, queryObject.pageNumber, queryObject.limit, totalItems);
        }
    }
}
