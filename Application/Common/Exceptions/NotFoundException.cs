using System.Net;

namespace Application.Common.Exceptions
{
    public class NotFoundException : ExceptionBase
    {
        public override int Status => (int)HttpStatusCode.NotFound;

        public NotFoundException(string key, object value = null!) : base($"{key} {value ?? string.Empty} wans't found") { }
    }
}
