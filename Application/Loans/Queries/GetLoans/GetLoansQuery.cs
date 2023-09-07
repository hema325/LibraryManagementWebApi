namespace Application.Loans.Queries.GetLoans
{
    public record GetLoansQuery(int PageNumber, int PageSize): IRequest<PaginatedList<LoanDto>>;
}
