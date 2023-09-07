namespace Application.Authors.Queries.GetAuthors
{
    public record GetAuthorsQuery(int PageNumber, int PageSize): IRequest<PaginatedList<AuthorDto>>;
}
