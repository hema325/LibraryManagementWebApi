namespace Application.Clients.Commands.UpdateClient
{
    public record UpdateClientCommand(int Id, string Name, string Notes, List<string> PhoneNumbers, int GenderId, int StatusId): IRequest;
}
