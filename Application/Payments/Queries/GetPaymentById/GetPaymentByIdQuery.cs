namespace Application.Payments.Queries.GetPaymentById
{
    public record GetPaymentByIdQuery(int Id): IRequest<PaymentDto>;
}
