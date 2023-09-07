using Microsoft.AspNetCore.Http;

namespace Application.Common.Interfaces
{
    public interface IFileStorage
    {
        void Remove(string path);
        Task<string> UploadAsync(IFormFile file, CancellationToken cancellationToken = default);
    }
}
