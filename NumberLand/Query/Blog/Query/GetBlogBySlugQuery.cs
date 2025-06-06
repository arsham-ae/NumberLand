﻿using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Query.Blog.Query
{
    public class GetBlogBySlugQuery : IRequest<BlogDTO>
    {
        public string Slug { get; set; }
        public GetBlogBySlugQuery(string slug)
        {
            Slug = slug;
        }
    }
}
