namespace WebAPI.Infrastructure.Data
{
    public class CustomHeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.OnStarting(() =>
            {
                context.Response.Headers.Add("X-Custom-Header", "Value");
                return Task.CompletedTask;
            });

            await _next(context);
        }
    }
}
