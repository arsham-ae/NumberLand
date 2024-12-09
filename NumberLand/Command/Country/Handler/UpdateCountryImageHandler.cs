using AutoMapper;
using MediatR;
using NumberLand.Command.Country.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Utility;

namespace NumberLand.Command.Country.Handler
{
    public class UpdateCountryImageHandler : IRequestHandler<UpdateCountryImageCommand, CommandsResponse<CountryDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly SaveImageHelper _saveImageHelper;
        public UpdateCountryImageHandler(IUnitOfWork unitOfWork, IMapper mapper, SaveImageHelper saveImageHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _saveImageHelper = saveImageHelper;
        }
        public async Task<CommandsResponse<CountryDTO>> Handle(UpdateCountryImageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var country = await _unitOfWork.country.Get(c => c.id == request.Id);
                if (country == null)
                {
                    return new CommandsResponse<CountryDTO>
                    {
                        status = "Fail",
                        message = $"Country With Id {request.Id} Not Found!"
                    };
                }
                country.flagIcon = await _saveImageHelper.SaveImage(request.File, "flags");
                await _unitOfWork.Save();
                return new CommandsResponse<CountryDTO>
                {
                    status = "Success",
                    message = $"Country Image With Id {request.Id} Updated Successfully.",
                    data = _mapper.Map<CountryDTO>(country)
                };
            }
            catch (Exception ex)
            {
                return new CommandsResponse<CountryDTO>
                {
                    status = "Fail",
                    message = ex.InnerException.Message
                };
            }
        }
    }
}
