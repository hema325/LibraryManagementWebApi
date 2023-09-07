namespace Application.Payments.Commands.CreatePayment
{
    internal class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreatePaymentCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = new Payment
            {
                Amount = request.Amount,
                Notes = request.Notes,
                ClientId = request.ClientId,
                FineId = request.FineId
            };

            payment.AddDomainEvent(new EntityCreatedEvent(payment));

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync(cancellationToken);

            return payment.Id;
        }
    }
}
