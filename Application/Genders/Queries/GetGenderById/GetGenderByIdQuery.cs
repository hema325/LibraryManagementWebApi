namespace Application.Genders.Queries.GetGenderById
{
    public record GetGenderByIdQuery(int Id): IRequest<GenderDto>;
}
