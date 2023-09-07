namespace Application.Clients.Commands.CreateClient
{
    public record CreateClientCommand(string Name, string Notes, List<string> PhoneNumbers,int GenderId, int StatusId): IRequest<int>;
}
