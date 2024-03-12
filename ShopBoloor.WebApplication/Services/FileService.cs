using Shared.Application.Services;

namespace ShopBoloor.WebApplication.Services
{
    internal class FileService : IFileService
    {
        private readonly IWebHostEnvironment _environment;
        public FileService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public bool DeleteImage(string route)
        {
            var directory = $"{_environment.WebRootPath}//{route}";
            if (File.Exists(directory))
            {
                try
                {
                    File.Delete(directory);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }

        public bool ResizeImage(string imageName, string Folder, int newSize)
        {
            var directory = $"{_environment.WebRootPath}/Images/{Folder}/{imageName}";
            var newDirectory = $"{_environment.WebRootPath}/Images/{Folder}/{newSize}";

            if (!Directory.Exists(newDirectory))
                Directory.CreateDirectory(newDirectory);

            try
            {
                using var image = Image.Load(directory);
                image.Mutate(x => x.Resize(newSize, 0));

                image.Save(newDirectory + "/" + imageName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string UploadImage(IFormFile file, string folder)
        {
            if (file == null) return "";

            var directory = $"{_environment.WebRootPath}//Images//{folder}";

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            var fileName = $"{Guid.NewGuid()}{file.FileName}";

            var path = $"{directory}//{fileName}";
            using (var output = File.Create(path))
            {
                try
                {
                    file.CopyTo(output);
                    return fileName;
                }
                catch
                {
                    return "";
                }
            }
        }
    }
}
