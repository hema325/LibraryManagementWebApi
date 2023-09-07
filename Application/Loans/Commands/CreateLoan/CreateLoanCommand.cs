namespace Application.Loans.Commands.CreateLoan
{
    public record CreateLoanCommand(string Notes, int BookId, int ClientId, DateTime? ReturnDate): IRequest<int>;
}
