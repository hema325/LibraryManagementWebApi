namespace Application.ClientStatuses.Commands.UpdateClientStatus
{
    public record UpdateClientStatusCommand(int Id, string Name, string Notes) : IRequest;
}
