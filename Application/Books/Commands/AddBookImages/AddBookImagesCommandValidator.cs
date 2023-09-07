namespace Application.Books.Commands.AddBookImages
{
    public class AddBookImagesCommandValidator: AbstractValidator<AddBookImagesCommand>
    {
        public AddBookImagesCommandValidator()
        {
            RuleFor(c => c.Images)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .ForEach(builder =>
                {
                    builder.Cascade(CascadeMode.Stop);
                    builder.NotEmpty();
                    builder.IsImage();
                });
        }
    }
}
