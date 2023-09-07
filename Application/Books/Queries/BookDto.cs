using Application.Authors.Queries;
using Application.Categories.Queries;

namespace Application.Books.Queries
{
    public class BookDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public int OwnedQuantity { get; init; }
        public int loanedQuantity {get;init;}
        public int Available => OwnedQuantity - loanedQuantity;
        public DateTime ReleasedAt { get; init; }
        public string Notes { get; init; }
        public List<string> Images { get; init; }

        public CategoryDto Category { get; init; }
        public List<AuthorDto> Authors { get; init;}


        private class Mapping: Profile
        {
            public Mapping()
            {
                CreateMap<Book, BookDto>()
                    .ForMember(dto => dto.Images, o => o.MapFrom(b => b.Images.Select(i => i.Path)))
                    .ForMember(dto => dto.loanedQuantity, o => o.MapFrom(b => b.Loans.Count(l => l.ReturnDate == null)));
            }
        }
    }
}
