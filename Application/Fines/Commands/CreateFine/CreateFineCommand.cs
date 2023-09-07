namespace Application.Fines.Commands.CreateFine
{
    public record CreateFineCommand(decimal Amount, string Notes, int ClientId, int LoanId) : IRequest<int>;
}
