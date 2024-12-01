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
            CreateMap<NumberModel, NumberDTO>()
                .ForMember(dest => dest.numberSlug, opt => opt.MapFrom(src => src.slug));
            CreateMap<CreateNumberDTO, NumberModel>();
            CreateMap<UpdateNumberCommand, NumberModel>();
            CreateMap<CreateBlogDTO, BlogModel>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.blogId))
                .ForMember(dest => dest.title, opt => opt.MapFrom(src => src.blogTitle))
                .ForMember(dest => dest.content, opt => opt.MapFrom(src => src.blogContent))
                .ForMember(dest => dest.authorId, opt => opt.MapFrom(src => src.blogAuthorId))
                .ForMember(dest => dest.blogCategories, opt => opt.MapFrom(src => src.blogCategories.Select(id => new BlogCategoryJoinModel
                {
                    categoryId = id
                })))
                .ForMember(dest => dest.createAt, opt => opt.MapFrom(src => src.createAt))
                .ForMember(dest => dest.updateAt, opt => opt.MapFrom(src => src.updateAt))
                .ForMember(dest => dest.publishedAt, opt => opt.MapFrom(src => src.publishedAt))
                .ForMember(dest => dest.isPublished, opt => opt.MapFrom(src => src.blogIsPublished));

            CreateMap<BlogModel, BlogDTO>()
                .ForMember(dest => dest.blogId, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.blogSlug, opt => opt.MapFrom(src => src.slug))
                .ForMember(dest => dest.blogTitle, opt => opt.MapFrom(src => src.title))
                .ForMember(dest => dest.blogContent, opt => opt.MapFrom(src => src.content))
                .ForMember(dest => dest.blogAuthor, opt => opt.MapFrom(src => src.author))
                .ForMember(dest => dest.blogCategories, opt => opt.MapFrom(src => src.blogCategories.Select(bc => new BlogCategoryDTO
                {
                    blogCategoryId = bc.category.id,
                    blogCategoryName = bc.category.name,
                    blogCategorySlug = bc.category.slug,
                    blogCategoryDescription = bc.category.description
                })))
                .ForMember(dest => dest.blogFeaturedImagePath, opt => opt.MapFrom(src => src.featuredImagePath))
                .ForMember(dest => dest.createAt, opt => opt.MapFrom(src => src.createAt))
                .ForMember(dest => dest.updateAt, opt => opt.MapFrom(src => src.updateAt))
                .ForMember(dest => dest.publishedAt, opt => opt.MapFrom(src => src.publishedAt))
                .ForMember(dest => dest.blogIsPublished, opt => opt.MapFrom(src => src.isPublished));
            CreateMap<BlogCategoryModel, BlogCategoryDTO>()
                .ForMember(dest => dest.blogCategoryId, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.blogCategoryName, opt => opt.MapFrom(src => src.name))
                .ForMember(dest => dest.blogCategorySlug, opt => opt.MapFrom(src => src.slug))
                .ForMember(dest => dest.blogCategoryDescription, opt => opt.MapFrom(src => src.description));
            CreateMap<BlogCategoryModel, BlogCategoryDTO>()
                .ForMember(dest => dest.blogCategoryId, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.blogCategoryName, opt => opt.MapFrom(src => src.name))
                .ForMember(dest => dest.blogCategorySlug, opt => opt.MapFrom(src => src.slug))
                .ForMember(dest => dest.blogCategoryDescription, opt => opt.MapFrom(src => src.description));

            CreateMap<PageDTO, PageeModel>();
            CreateMap<PageeModel, PageDTO>();
            CreateMap<CreatePageDTO, PageeModel>();
            CreateMap<PageCategoryDTO, PageCategoryModel>();
            CreateMap<PageCategoryModel, PageCategoryDTO>();
            CreateMap<CreatePageCategoryDTO, PageCategoryModel>();

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
