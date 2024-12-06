using AutoMapper;
using MediatR;
using NumberLand.DataAccess.Data;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Numbers;
using NumberLand.Query.Country.Query;

namespace NumberLand.Query.Country.Handler
{
    public class GetCountryByIdHandler : IRequestHandler<GetCountryByIdQuery, CountryDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetCountryByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CountryDTO> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
        {
            var country = _mapper.Map<CountryDTO>(await _unitOfWork.country.Get(c => c.id == request.Id));
            return country == null ? null : country;
        }
    }
}
