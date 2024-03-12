using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Application.Services
{
    public interface IFileService
    {
        string UploadImage(IFormFile file, string folder);
        bool DeleteImage(string route);
        bool ResizeImage(string imageName, string Folder, int newSize);
    }
}
