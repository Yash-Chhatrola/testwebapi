namespace apiexample.Middelware
{
    public class MyCustomMiddelware
    {
        private readonly RequestDelegate next;

        public MyCustomMiddelware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/api/Home"))
            {
                await next(context);
                return;
            }
            if(context.Request.Path == "/api/Stu/Post")
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("No accissible:");
                return;
            }
            await next(context);
        }
    }
}
