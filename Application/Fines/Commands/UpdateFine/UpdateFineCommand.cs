namespace Application.Fines.Commands.UpdateFine
{
    public record UpdateFineCommand(int Id, decimal Amount, string Notes, int ClientId, int LoanId): IRequest;
}
