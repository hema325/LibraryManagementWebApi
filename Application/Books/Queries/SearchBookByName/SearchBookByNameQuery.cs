namespace Application.Books.Queries.SearchBookByName
{
    public record SearchBookByNameQuery(string Name, int PageNumber, int PageSize): IRequest<PaginatedList<BookDto>>;
}
