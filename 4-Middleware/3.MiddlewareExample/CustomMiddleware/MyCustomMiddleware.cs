namespace MiddlewareExample.CustomMiddleware
{
    public class MyCustomMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync($"My custom middleware Starts\n");
            await next(context);
            await context.Response.WriteAsync($"My custom middleware Ends\n");
        }
    }


    public static class CustomMiddlewareExtension
    {
        public static IApplicationBuilder UseMyCustomMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyCustomMiddleware>();
        }
    }

    public class HelloCustomMiddleware
    {
        private readonly RequestDelegate _next;

        public HelloCustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            context.Request.Query.TryGetValue("first-name", out var firstName);
            context.Request.Query.TryGetValue("last-name", out var lastName);
            string fullName = $"{firstName[0]} {lastName[0]}";
            await context.Response.WriteAsync($"My full name is: {fullName}\n");
            await _next(context);
        }
    }

    public static class HelloCustomMiddlewareExtension
    {
        public static IApplicationBuilder UseHelloCustomMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HelloCustomMiddleware>();
        }
    }
}
