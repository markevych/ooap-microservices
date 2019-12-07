using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace IdentityService.Services.Services
{
    public interface IFileService
    {
        Task<string> UploadFile(IFormFile file);
        Task<string> UploadFile(string folderPath, IFormFile file);
        void DeleteFile(string fullPath);
        byte[] GetFile(string filePath);
        string GetValidUrl(IUrlHelper urlHelper, string path);
    }

    public class FileService : IFileService
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public FileService(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }


        public byte[] GetFile(string filePath)
        {
            if (filePath == null || !File.Exists(filePath)) return null;

            try
            {
                return File.ReadAllBytes(filePath);
            }
            catch
            {
                return null;
            }
        }

        public string GetValidUrl(IUrlHelper urlHelper, string path)
        {
            return path == null ? null : urlHelper.Link("DefaultApi", new { Path = $"storage/{path}" });
        }

        public async Task<string> UploadFile(IFormFile file)
        {
            if (file == null) return null;

            if (file.Length > 0)
            {
                var fileExtension = file.FileName != "blob"
                    ? Path.GetExtension(file.FileName)
                    : ".png";
                var fileName = Path.GetFileName($"{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}{fileExtension}");
                var filePath = Path.GetTempFileName();

                await using var stream = File.Create(filePath);
                await file.CopyToAsync(stream);

                return Path.Combine(filePath, fileName);
            }

            return null;
        }

        public async Task<string> UploadFile(string folderPath, IFormFile file)
        {
            var folder = $"{_hostingEnvironment.ContentRootPath}/{folderPath}";
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            var fileExtension = file.FileName != "blob"
                ? Path.GetExtension(file.FileName)
                : ".png";
            var fileName = Path.GetFileName($"{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}{fileExtension}");
            var filePath = Path.Combine(folder, fileName);
            
            await using var stream = File.Create(filePath);
            await file.CopyToAsync(stream);

            return Path.Combine(folderPath, fileName);
        }

        public void DeleteFile(string fullPath)
        {
            if (fullPath != null && File.Exists($"{_hostingEnvironment.ContentRootPath}/{fullPath}"))
            {
                File.Delete(fullPath);
            }
        }
    }
}