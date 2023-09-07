namespace Application.Books.Commands.UpdateBook
{
    public record UpdateBookCommand(int Id,
                                    string Name,
                                    int OwnedQuantity,
                                    DateTime ReleasedAt,
                                    string Notes,
                                    int CategoryId,
                                    List<int> AuthorsIds) : IRequest;
}
