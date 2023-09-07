namespace Application.Payments.Commands.UpdatePayment
{
    public record UpdatePaymentCommand(int Id, decimal Amount, string? Notes, int ClientId, int FineId): IRequest;
}
