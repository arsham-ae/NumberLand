﻿using AutoMapper;
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
                .ForMember(dest => dest.slug, opt => opt.MapFrom(src => src.blogSlug))
                .ForMember(dest => dest.title, opt => opt.MapFrom(src => src.blogTitle))
                .ForMember(dest => dest.content, opt => opt.MapFrom(src => src.blogContent))
                .ForMember(dest => dest.preview, opt => opt.MapFrom(src => src.blogPreview))
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
                .ForMember(dest => dest.blogPreview, opt => opt.MapFrom(src => src.preview))
                .ForMember(dest => dest.blogAuthor, opt => opt.MapFrom(src => src.author))
                .ForMember(dest => dest.blogCategories, opt => opt.MapFrom(src => src.blogCategories.Select(bc => new BlogCategoryDTO
                {
                    blogCategoryId = bc.category.id,
                    blogCategoryName = bc.category.name,
                    blogCategorySlug = bc.category.slug,
                    blogCategoryDescription = bc.category.description,
                    colorCode = bc.category.colorCode,
                    icon = bc.category.icon
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
            CreateMap<CreateBlogCategoryDTO, BlogCategoryModel>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.blogCategoryId))
                .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.blogCategoryName))
                .ForMember(dest => dest.slug, opt => opt.MapFrom(src => src.blogCategorySlug))
                .ForMember(dest => dest.description, opt => opt.MapFrom(src => src.blogCategoryDescription));

            CreateMap<PageDTO, PageeModel>();
            CreateMap<PageeModel, PageDTO>();
            CreateMap<CreatePageDTO, PageeModel>();
            CreateMap<PageCategoryDTO, PageCategoryModel>();
            CreateMap<PageCategoryModel, PageCategoryDTO>();
            CreateMap<CreatePageCategoryDTO, PageCategoryModel>();

            CreateMap<CreateAuthorDTO, AuthorModel>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.authorId))
                .ForMember(dest => dest.slug, opt => opt.MapFrom(src => src.authorSlug))
                .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.authorName))
                .ForMember(dest => dest.description, opt => opt.MapFrom(src => src.authorDescription));
            CreateMap<CreateAuthorCommand, AuthorModel>();
            CreateMap<AuthorModel, AuthorDTO>()
                .ForMember(dest => dest.authorId, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.authorSlug, opt => opt.MapFrom(src => src.slug))
                .ForMember(dest => dest.authorName, opt => opt.MapFrom(src => src.name))
                .ForMember(dest => dest.authorDescription, opt => opt.MapFrom(src => src.description))
                .ForMember(dest => dest.imagePath, opt => opt.MapFrom(src => src.imagePath));
            CreateMap<CreateOperatorDTO, OperatorModel>();
            CreateMap<OperatorModel, OperatorModel>();
            CreateMap<ApplicationModel, ApplicationDTO>()
            .ForMember(dest => dest.appId, opt => opt.MapFrom(src => src.id))
            .ForMember(dest => dest.appSlug, opt => opt.MapFrom(src => src.slug))
            .ForMember(dest => dest.appName, opt => opt.MapFrom(src => src.name))
            .ForMember(dest => dest.appContent, opt => opt.MapFrom(src => src.content));
            CreateMap<CreateApplicationDTO, ApplicationModel>()
            .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.appId))
            .ForMember(dest => dest.slug, opt => opt.MapFrom(src => src.appSlug))
            .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.appName))
            .ForMember(dest => dest.content, opt => opt.MapFrom(src => src.appContent));

            CreateMap<CountryModel, CountryDTO>()
                .ForMember(dest => dest.countryId, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.countrySlug, opt => opt.MapFrom(src => src.slug))
                .ForMember(dest => dest.countryName, opt => opt.MapFrom(src => src.name))
                .ForMember(dest => dest.countryContent, opt => opt.MapFrom(src => src.content))
                .ForMember(dest => dest.countryFlagIcon, opt => opt.MapFrom(src => src.flagIcon));

            CreateMap<CreateCountryDTO, CountryModel>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.countryId))
                .ForMember(dest => dest.slug, opt => opt.MapFrom(src => src.countrySlug))
                .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.countryName))
                .ForMember(dest => dest.content, opt => opt.MapFrom(src => src.countryContent));
        }
    }
}
