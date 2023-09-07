namespace Application.Loans.Commands.UpdateLoan
{
    public record UpdateLoanCommand(int Id, string Notes, int BookId, int ClientId, DateTime? ReturnDate): IRequest;
}
