using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.FileStorage
{
    internal class LocalFileStorageService: IFileStorage
    {
        private readonly IWebHostEnvironment _environment;

        public LocalFileStorageService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> UploadAsync(IFormFile file, CancellationToken cancellationToken = default)
        {
            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var fileType = Capitalize(file.ContentType.Substring(0, file.ContentType.IndexOf('/')) + 's');

            var fileRelativePath = Path.Combine(fileType, fileName);
            var fileAbsolutePath = Path.Combine(_environment.WebRootPath, fileRelativePath);

            var directoryAbsolutePath = Path.Combine(_environment.WebRootPath, fileType);

            if (!Directory.Exists(directoryAbsolutePath))
                Directory.CreateDirectory(directoryAbsolutePath);

            using var fileStream = File.Create(fileAbsolutePath);
            await file.CopyToAsync(fileStream, cancellationToken);

            return fileRelativePath;
        }

        public void Remove(string relativePath)
        {
            var absolutePath = Path.Combine(_environment.WebRootPath, relativePath);
            File.Delete(absolutePath);
        }

        private string Capitalize(string text)
        {
            var words = text.Split(' ');
            var capitalizedWords = words.Select(w => char.ToUpper(w[0]) + w.Substring(1));

            return string.Join(" ", capitalizedWords);
        }
    }
}
