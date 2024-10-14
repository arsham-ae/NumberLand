using AutoMapper;
using NumberLand.DataAccess.DTOs;
using NumberLand.Models.Blogs;
using NumberLand.Models.Numbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberLand.DataAccess.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateNumberDTO, NumberModel>();
            CreateMap<NumberModel, NumberDTO>();
            CreateMap<CreateBlogDTO, BlogModel>()
                .ForMember(dest => dest.blogCategories, opt => opt.Ignore());
            CreateMap<BlogModel, BlogDTO>();
        }
    }
}
