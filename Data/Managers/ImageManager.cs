using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Blog.Data.Managers
{
    public class ImageManager : IImageManager
    {
        private readonly string _savePath;

        public ImageManager(IConfiguration config)
        {
            _savePath = config["Paths:Images"];
        }

        public bool DeleteImage(string image)
        {
            if (string.IsNullOrEmpty(image))
                return false;

            var imagePath = Path.Combine(_savePath, image);

            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
                return true;
            }

            return false;
        }

        public FileStream GetImageStream(string image)
        {
            var imagePath = Path.Combine(_savePath, image);
            return new FileStream(imagePath, FileMode.Open, FileAccess.Read);
        }

        public async Task<string> SaveImageStream(IFormFile image)
        {
            var saveDir = Path.Combine(_savePath);

            if (!Directory.Exists(saveDir))
                Directory.CreateDirectory(saveDir);

            var mimeType = image.FileName.Substring(image.FileName.IndexOf('.'));

            var fileName = $"{Guid.NewGuid()}{mimeType}";

            var fullPath = Path.Combine(saveDir, fileName);

            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            using (var imageStream = await Image.LoadAsync(image.OpenReadStream()))
            {
                var clone = imageStream.Clone(operation => operation.Resize(800, 500));
                await clone.SaveAsJpegAsync(fileStream, new JpegEncoder { Quality = 100 });
            }

            return fileName;
        }
    }
}
