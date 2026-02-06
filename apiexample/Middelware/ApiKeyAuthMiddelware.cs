namespace apiexample.Middelware
{
    public class ApiKeyAuthMiddelware
    {
        private readonly RequestDelegate next;
        private const string apikey = "yash1234567";
        public ApiKeyAuthMiddelware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var headers = context.Request.Headers;
            if(headers.TryGetValue("x-ApiKey",out var extractedKeyValue))
            {
                if(extractedKeyValue == apikey)
                {
                    await next(context);
                }
                else
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Unauthorized - Request");
                    return;
                }
            }
            else
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API key was not provided.");
                return;
            }
        }
    }
}
