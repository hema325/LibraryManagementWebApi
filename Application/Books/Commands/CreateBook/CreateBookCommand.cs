using Microsoft.AspNetCore.Http;

namespace Application.Books.Commands.CreateBook
{
    public record CreateBookCommand(string Name,
                                    int OwnedQuantity,
                                    DateTime ReleasedAt,
                                    string Notes,
                                    int CategoryId,
                                    List<int> AuthorsIds) : IRequest<int>;
}
