﻿using Microsoft.EntityFrameworkCore;

namespace Application.Books.Commands.UpdateBook
{
    public class UpdateBookCommandValidator: AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator(IApplicationDbContext context)
        {
            RuleFor(c => c.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MaximumLength(256)
                .MustAsync(async (cmd, n, ct) => !await context.Books.AnyAsync(c => c.Name == n && c.Id != cmd.Id, ct)).WithMessage("'{PropertyName}' already exists.");

            RuleFor(c => c.OwnedQuantity)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(c => c.CategoryId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MustAsync(async (id, ct) => await context.Categories.AnyAsync(c => c.Id == id, ct)).WithMessage("'{PropertyName}' isn't valid");

            RuleFor(c => c.AuthorsIds)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .ForEach(builder =>
                {
                    builder.Cascade(CascadeMode.Stop);
                    builder.NotEmpty();
                    builder.MustAsync(async (id, ct) => await context.Authors.AnyAsync(a => a.Id == id, ct)).WithMessage("'{PropertyName}' isn't valid");
                });
        }
    }
}
