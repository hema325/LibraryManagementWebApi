namespace Application.Authors.Command.CreateAuthor
{
    public record CreateAuthorCommand(string Name, string Notes, int GenderId): IRequest<int>;
}

