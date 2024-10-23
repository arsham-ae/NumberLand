using AutoMapper;
using NumberLand.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.Models.Blogs;
using NumberLand.Models.Numbers;
using NumberLand.Models.Pages;

namespace NumberLand.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateNumberDTO, NumberModel>();
            CreateMap<NumberModel, NumberDTO>();
            CreateMap<CreateBlogDTO, BlogModel>()
                .ForMember(dest => dest.blogCategories, opt => opt.Ignore());
            CreateMap<BlogModel, BlogDTO>()
                .ForMember(dest => dest.blogCategories, opt => opt.MapFrom(src => src.blogCategories));
            CreateMap<BlogCategoryJoinModel, BlogCategoryDTO>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.categoryId))
                .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.category.name))
                .ForMember(dest => dest.description, opt => opt.MapFrom(src => src.category.description));
            CreateMap<BlogCategoryDTO, BlogCategoryModel>();
            CreateMap<BlogCategoryModel, BlogCategoryDTO>();
            CreateMap<PageDTO, PageeModel>();
            CreateMap<PageeModel, PageDTO>();
            CreateMap<CreatePageDTO, PageeModel>();
            CreateMap<PageCategoryDTO, PageCategoryModel>();
            CreateMap<PageCategoryModel, PageCategoryDTO>();
            CreateMap<CreatePageCategoryDTO, PageCategoryModel>();
            CreateMap<AuthorDTO, AuthorModel>();
            CreateMap<CreateNumberCommand, NumberModel>();
            CreateMap<UpdateNumberCommand, NumberModel>();
        }
    }
}
