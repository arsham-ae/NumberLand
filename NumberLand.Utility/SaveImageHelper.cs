using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace NumberLand.Utility
{
    public class SaveImageHelper
    {
        private readonly IWebHostEnvironment _environment;
        public SaveImageHelper(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public async Task<string> SaveImage(IFormFile file, string path)
        {
            try
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp", ".svg" };
                var fileExtension = Path.GetExtension(file.FileName);
                if (!allowedExtensions.Contains(fileExtension, StringComparer.OrdinalIgnoreCase))
                {
                    throw new InvalidOperationException($"File type {fileExtension} is not allowed.");
                }
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "images", path);
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

                return Path.Combine($"images/{path}", uniqueFileName).Replace("\\", "/");
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }
    }
}
