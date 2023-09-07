namespace Application.Loans.Queries.GetLoanById
{
    public record GetLoanByIdQuery(int Id): IRequest<LoanDto>;
}
