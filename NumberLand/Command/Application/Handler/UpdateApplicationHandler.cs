﻿using AutoMapper;
using Markdig;
using MediatR;
using NumberLand.Command.Application.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;

namespace NumberLand.Command.Application.Handler
{
    public class UpdateApplicationHandler : IRequestHandler<UpdateApplicationCommand, CommandsResponse<ApplicationDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        public UpdateApplicationHandler(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment environment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _environment = environment;
        }

        public async Task<CommandsResponse<ApplicationDTO>> Handle(UpdateApplicationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var pipeLine = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
                var app = await _unitOfWork.application.Get(app => app.id == request.Id);
                if (app == null)
                {
                    return new CommandsResponse<ApplicationDTO>
                    {
                        status = "Fail",
                        message = "app Not Found!"
                    };
                }
                if (request.File != null && request.File.Length > 0)
                {
                    app.appIcon = await SaveAppIcon(request.File);
                }
                request.PatchDoc.ApplyTo(app);
                app.content = Markdown.ToHtml(app.content, pipeLine);
                await _unitOfWork.Save();

                return new CommandsResponse<ApplicationDTO>
                {
                    status = "Success",
                    message = $"Application With Id {request.Id} Updated SuccessFully!",
                    data = _mapper.Map<ApplicationDTO>(app)
                };

            }
            catch (Exception ex)
            {
                return new CommandsResponse<ApplicationDTO>
                {
                    status = "Fail",
                    message = ex.InnerException.Message
                };
            }
        }
        private async Task<string> SaveAppIcon(IFormFile file)
        {
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "images", "apps");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return Path.Combine("images/apps", uniqueFileName).Replace("\\", "/");
        }
    }
}