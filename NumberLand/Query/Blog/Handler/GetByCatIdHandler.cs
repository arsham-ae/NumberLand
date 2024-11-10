using AutoMapper;
using MediatR;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Query.Blog.Query;

namespace NumberLand.Query.Blog.Handler
{
    public class GetByCatIdHandler : IRequestHandler<GetByCatIdQuery, IEnumerable<BlogDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetByCatIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<BlogDTO>> Handle(GetByCatIdQuery request, CancellationToken cancellationToken)
        {
            var get = _mapper.Map<List<BlogDTO>>((await _unitOfWork.blog.GetAll(includeProp: "author, blogCategories.category"))
        .Where(b => b.blogCategories != null && b.blogCategories.Any(bc => bc.categoryId == request.CatId))
        .ToList());
            return get == null ? null : get;
        }
    }
}
