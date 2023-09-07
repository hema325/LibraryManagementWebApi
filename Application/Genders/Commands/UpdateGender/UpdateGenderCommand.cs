namespace Application.Genders.Commands.UpdateGender
{
    public record UpdateGenderCommand(int Id, string Name): IRequest;
}
