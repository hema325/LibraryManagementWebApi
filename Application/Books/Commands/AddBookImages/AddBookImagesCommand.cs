using Microsoft.AspNetCore.Http;

namespace Application.Books.Commands.AddBookImages
{
    public record AddBookImagesCommand(int Id, List<IFormFile> Images): IRequest;
}
