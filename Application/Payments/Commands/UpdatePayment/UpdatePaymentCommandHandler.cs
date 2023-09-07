namespace Application.Payments.Commands.UpdatePayment
{
    internal class UpdatePaymentCommandHandler : IRequestHandler<UpdatePaymentCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdatePaymentCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = await _context.Payments.FindAsync(request.Id, cancellationToken);
            
            if(payment == null)
                throw new NotFoundException(nameof(Payment), request.Id);

            payment.Amount = request.Amount;
            payment.Notes = request.Notes;
            payment.ClientId = request.ClientId;
            payment.FineId = request.FineId;

            payment.AddDomainEvent(new EntityCreatedEvent(payment));
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
