namespace Application.Payments.Commands.DeletePayment
{
    internal class DeletePaymentCommandHandler : IRequestHandler<DeletePaymentCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeletePaymentCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = await _context.Payments.FindAsync(request.Id, cancellationToken);

            if (payment == null)
                throw new NotFoundException(nameof(Payment), request.Id);

            payment.AddDomainEvent(new EntityDeletedEvent(payment));

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
