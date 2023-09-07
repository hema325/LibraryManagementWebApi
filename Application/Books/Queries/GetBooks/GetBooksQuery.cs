namespace Application.Books.Queries.GetBooks
{
    public record GetBooksQuery(int PageNumber, int PageSize) : IRequest<PaginatedList<BookDto>>;
}
