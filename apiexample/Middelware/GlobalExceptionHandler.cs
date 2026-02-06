using Microsoft.AspNetCore.Diagnostics;

namespace apiexample.Middelware
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var errorResponce = new
            {
                StatusCode = 500,
                Message = "An unexpected error occurred",
                Detailed = exception.Message
            };

            httpContext.Response.StatusCode = 500;
            httpContext.Response.ContentType = "application/json";

            await httpContext.Response.WriteAsJsonAsync(errorResponce,cancellationToken);
            return true;
        }
    }
}
