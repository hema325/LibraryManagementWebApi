using Microsoft.AspNetCore.Http;

namespace Application.Common.Extensions
{
    internal static class IRuleBuilderExtensions
    {
        public static IRuleBuilderOptions<TObject, IFormFile> IsImage<TObject>(this IRuleBuilder<TObject,IFormFile> builder)
        {
            return builder.Must(f => f.ContentType.Contains("image"))
                .WithMessage("'{PropertyName}' must be an image");
        }
    }
}
