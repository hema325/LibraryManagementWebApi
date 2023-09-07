﻿using Microsoft.EntityFrameworkCore;

namespace Application.Payments.Commands.CreatePayment
{
    public class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
    {
        public CreatePaymentCommandValidator(IApplicationDbContext context)
        {
            RuleFor(c => c.Amount)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .PrecisionScale(9, 2, false);

            RuleFor(c => c.FineId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MustAsync(async (i, ct) => await context.Fines.AnyAsync(f => f.Id == i, ct)).WithMessage("{PropertyName} is not valid");

            RuleFor(c => c.ClientId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MustAsync(async (i, ct) => await context.Clients.AnyAsync(clt => clt.Id == i, ct)).WithMessage("{PropertyName} is not valid");
        }
    }
}
