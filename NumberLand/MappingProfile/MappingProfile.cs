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
                .ForMember(dest => dest.blogCategories, opt => opt.Ignore())
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.blogId))
                .ForMember(dest => dest.title, opt => opt.MapFrom(src => src.blogTitle))
                .ForMember(dest => dest.content, opt => opt.MapFrom(src => src.blogContent))
                .ForMember(dest => dest.authorId, opt => opt.MapFrom(src => src.blogAuthorId));

            CreateMap<BlogModel, BlogDTO>()
                .ForMember(dest => dest.blogId, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.blogTitle, opt => opt.MapFrom(src => src.title))
                .ForMember(dest => dest.blogContent, opt => opt.MapFrom(src => src.content))
                .ForMember(dest => dest.blogCategories, opt => opt.MapFrom(src => src.blogCategories))
                .ForMember(dest => dest.blogAuthor, opt => opt.MapFrom(src => src.author))
                .ForMember(dest => dest.blogCategories, opt => opt.MapFrom(src => src.blogCategories))
                .ForMember(dest => dest.blogSlug, opt => opt.MapFrom(src => src.slug))
                .ForMember(dest => dest.blogFeaturedImagePath, opt => opt.MapFrom(src => src.featuredImagePath))
                .ForMember(dest => dest.blogIsPublished, opt => opt.MapFrom(src => src.isPublished));
            CreateMap<BlogCategoryJoinModel, BlogCategoryDTO>()
                .ForMember(dest => dest.blogCategoryId, opt => opt.MapFrom(src => src.categoryId))
                .ForMember(dest => dest.blogCategoryName, opt => opt.MapFrom(src => src.category.name))
                .ForMember(dest => dest.blogCategoryDescription, opt => opt.MapFrom(src => src.category.description));
            CreateMap<BlogCategoryDTO, BlogCategoryModel>();
            CreateMap<BlogCategoryModel, BlogCategoryDTO>()
                .ForMember(dest => dest.blogCategoryId, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.blogCategoryName, opt => opt.MapFrom(src => src.name))
                .ForMember(dest => dest.blogCategorySlug, opt => opt.MapFrom(src => src.slug))
                .ForMember(dest => dest.blogCategoryDescription, opt => opt.MapFrom(src => src.description));
            ;
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
