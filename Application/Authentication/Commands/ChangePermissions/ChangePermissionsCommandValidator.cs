using Domain.Enums;

namespace Application.Authentication.Commands.ChangePermissions
{
    public class ChangePermissionsCommandValidator: AbstractValidator<ChangePermissionsCommand>
    {
        public ChangePermissionsCommandValidator()
        {
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
