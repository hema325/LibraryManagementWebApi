using Domain.Enums;

namespace Application.Authentication.Commands.Register
{
    public class RegisterCommandValidator: AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(c => c.Roles)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .ForEach(builder =>
                {
                    builder.Cascade(CascadeMode.Stop);
                    builder.NotEmpty();
                    builder.IsEnumName(typeof(Roles)).WithMessage("'{PropertyName}' is not valid");
                });

            RuleFor(c => c.Permissions)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .ForEach(builder =>
                {
                    builder.Cascade(CascadeMode.Stop);
                    builder.NotEmpty();
                    builder.IsEnumName(typeof(Permissions)).WithMessage("'{PropertyName}' is not valid");
                });
        }
    }
}
