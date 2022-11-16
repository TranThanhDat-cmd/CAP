using System.Net.Http.Headers;


namespace CAP_Backend_Source.Modules.FileStorage.Service
{
    public interface IFileStorageService
    {
        void DeleteFile(string fileName);
        string GetFileUrl(string fileName);
        string? SaveFile(IFormFile file);
    }

    public class FileStorageService : IFileStorageService
    {
        private readonly string _userContentFolder;

        public FileStorageService()
        {
            _userContentFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
        }

        public string GetFileUrl(string fileName)
        {
            return fileName;
        }

        public string? SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName?.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            var filePath = Path.Combine(_userContentFolder, fileName);
            using var output = new FileStream(filePath, FileMode.Create);
            file.OpenReadStream().CopyTo(output);
            return fileName;
        }

        public void DeleteFile(string fileName)
        {
            var filePath = Path.Combine(_userContentFolder, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}