namespace Application.Payments.Queries.GetPayments
{
    public record GetPaymentsQuery(int PageNumber, int PageSize): IRequest<PaginatedList<PaymentDto>>;
}
