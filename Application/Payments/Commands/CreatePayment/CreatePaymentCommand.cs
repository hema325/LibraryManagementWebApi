namespace Application.Payments.Commands.CreatePayment
{
    public record CreatePaymentCommand(decimal Amount, string? Notes, int ClientId, int FineId) : IRequest<int>;
}
