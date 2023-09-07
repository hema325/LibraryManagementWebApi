namespace Application.Books.Commands.DeleteBookImages
{
    public record DeleteBookImageCommand(int BookId, List<string> ImagesPath): IRequest;
}
