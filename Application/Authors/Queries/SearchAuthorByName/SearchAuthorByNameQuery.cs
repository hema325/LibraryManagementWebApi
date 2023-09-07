namespace Application.Authors.Queries.SearchAuthorByName
{
    public record SearchAuthorByNameQuery(string Name, int PageNumber, int PageSize): IRequest<PaginatedList<AuthorDto>>;
}
