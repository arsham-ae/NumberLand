using AutoMapper;
using NumberLand.Command.Author.Command;
using NumberLand.Command.Number.Command;
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
            CreateMap<NumberModel, NumberDTO>()
                .ForMember(dest => dest.numberSlug, opt => opt.MapFrom(src => src.slug));
            CreateMap<CreateBlogDTO, BlogModel>()
                .ForMember(dest => dest.blogCategories, opt => opt.Ignore());
            CreateMap<BlogModel, BlogDTO>()
                .ForMember(dest => dest.blogCategories, opt => opt.MapFrom(src => src.blogCategories));
            CreateMap<BlogCategoryJoinModel, BlogCategoryDTO>()
                .ForMember(dest => dest.blogCategoryId, opt => opt.MapFrom(src => src.categoryId))
                .ForMember(dest => dest.blogCategoryName, opt => opt.MapFrom(src => src.category.name))
                .ForMember(dest => dest.blogCategoryDescription, opt => opt.MapFrom(src => src.category.description));
            CreateMap<BlogCategoryDTO, BlogCategoryModel>();
            CreateMap<BlogCategoryModel, BlogCategoryDTO>();
            CreateMap<PageDTO, PageeModel>();
            CreateMap<PageeModel, PageDTO>();
            CreateMap<CreatePageDTO, PageeModel>();
            CreateMap<PageCategoryDTO, PageCategoryModel>();
            CreateMap<PageCategoryModel, PageCategoryDTO>();
            CreateMap<CreatePageCategoryDTO, PageCategoryModel>();
            CreateMap<UpdateNumberCommand, NumberModel>();
            CreateMap<CreateAuthorDTO, AuthorModel>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.authorId))
                .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.authorName))
                .ForMember(dest => dest.description, opt => opt.MapFrom(src => src.authorDescription));
            CreateMap<CreateAuthorCommand, AuthorModel>();
            CreateMap<AuthorModel, AuthorDTO>()
                .ForMember(dest => dest.authorId, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.authorSlug, opt => opt.MapFrom(src => src.slug))
                .ForMember(dest => dest.authorName, opt => opt.MapFrom(src => src.name))
                .ForMember(dest => dest.authorDescription, opt => opt.MapFrom(src => src.description))
                .ForMember(dest => dest.imagePath, opt => opt.MapFrom(src => src.imagePath));
        }
    }
}
