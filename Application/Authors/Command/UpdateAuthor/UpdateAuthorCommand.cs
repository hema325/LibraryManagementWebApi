namespace Application.Authors.Command.UpdateAuthor
{
    public record UpdateAuthorCommand(int Id, string Name, string Notes, int GenderId) : IRequest;
}
