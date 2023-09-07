namespace Application.Genders.Commands.CreateGender
{
    public record CreateGenderCommand(string Name): IRequest<int>;
}
