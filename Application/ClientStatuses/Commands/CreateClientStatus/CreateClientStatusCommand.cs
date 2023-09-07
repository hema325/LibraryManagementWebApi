namespace Application.ClientStatuses.Commands.CreateClientStatus
{
    public record CreateClientStatusCommand(string Name, string Notes) : IRequest<int>;
}
