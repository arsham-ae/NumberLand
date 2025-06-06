﻿using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using NumberLand.DataAccess.DTOs;
using NumberLand.Models.Blogs;

namespace NumberLand.Command.Blog.Command
{
    public class UpdateBlogCategoryCommand : IRequest<CommandsResponse<BlogCategoryDTO>>
    {
        public int Id { get; set; }
        public JsonPatchDocument<BlogCategoryModel>? PatchDoc { get; set; }
        public IFormFile? File { get; set; }
        public UpdateBlogCategoryCommand(int id, JsonPatchDocument<BlogCategoryModel>? patchDoc, IFormFile? file)
        {
            Id = id;
            PatchDoc = patchDoc;
            File = file;
        }
    }
}
