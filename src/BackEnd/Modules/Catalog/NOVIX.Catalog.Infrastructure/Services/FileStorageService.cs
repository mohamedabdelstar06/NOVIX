using NOVIX.Catalog.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace NOVIX.Catalog.Infrastructure.Services
{
    public class FileStorageService : IFileStorageService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FileStorageService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> SaveFileAsync(Stream fileStream, string fileName)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);
            
            var safeFileName = fileName.Replace(" ", "-").ToLower();
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + safeFileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            
            using (var newFileStream = new FileStream(filePath, FileMode.Create))
            {
                await fileStream.CopyToAsync(newFileStream);
            }

            var request = _httpContextAccessor.HttpContext?.Request;
            if (request != null)
            {
                var baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";
                return $"{baseUrl}/uploads/{uniqueFileName}";
            }
            
            return $"/uploads/{uniqueFileName}";
        }
        
        public void DeleteFile(string fileUrl)
        {
            // Optional: delete logic
        }
    }
}
