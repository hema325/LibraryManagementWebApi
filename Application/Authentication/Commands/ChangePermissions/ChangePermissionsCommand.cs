namespace Application.Authentication.Commands.ChangePermissions
{
    public record ChangePermissionsCommand(int Id, List<string> Permissions): IRequest;
}
