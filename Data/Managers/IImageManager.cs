using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Data.Managers
{
    public interface IImageManager
    {
        Task<string> SaveImageStream(IFormFile image);
        FileStream GetImageStream(string image);
        bool DeleteImage(string image);
    }
}
