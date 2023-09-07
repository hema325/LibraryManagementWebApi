namespace Presentation.Middlewares
{
    public class GlobalExceptionHandlerMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (ValidationException ex)
            {
                var validationResult = new ValidationResult
                {
                    Status = ex.Status,
                    Message = ex.Message,
                    Errors = ex.Errors
                };

                context.Response.StatusCode = validationResult.Status;
                await context.Response.WriteAsJsonAsync(validationResult);
            }
            catch(Exception ex)
            {
                var exception = GetInnerException(ex);

                if (exception.GetType().IsAssignableTo(typeof(ExceptionBase))) 
                {
                    //handle custom exception
                    var exceptionBase = (ExceptionBase)exception;

                    var exceptionBaseResult = new ExceptionResult
                    {
                        Status = exceptionBase.Status,
                        Message = exceptionBase.Message
                    };

                    context.Response.StatusCode = exceptionBaseResult.Status;
                    await context.Response.WriteAsJsonAsync(exceptionBaseResult);
                }
                else
                {
                    //handle unknwon exception
                    var exceptionResult = new ExceptionResult
                    {
                        Status = 500,
                        Message = exception.Message
                    };

                    context.Response.StatusCode = exceptionResult.Status;
                    await context.Response.WriteAsJsonAsync(exceptionResult);
                }
            }
        }

        private Exception GetInnerException(Exception exception)
        {
            if (exception.InnerException == null)
                return exception;

            return GetInnerException(exception.InnerException);
        }
    }
}
