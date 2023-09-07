namespace Application.Books.Queries.GetBookById
{
    public record GetBookByIdQuery(int Id): IRequest<BookDto>;
}
