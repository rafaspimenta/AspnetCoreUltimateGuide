using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace LoginMiddleware
{
    public class MyAuthenticationMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var stream = new StreamReader(context.Request.Body);
            var body = await stream.ReadToEndAsync();
            var bodyAsDic = QueryHelpers.ParseQuery(body);

            bodyAsDic.TryGetValue("email", out var emails);
            bodyAsDic.TryGetValue("password", out var passwords);

            if (emails.Count == 0 && passwords.Count == 0)
            {
                context.Response.StatusCode = StatusCodes.Status200OK;
                await context.Response.WriteAsync($"No response");
                await next(context);
                return;
            }

            if (emails.Count == 0)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync($"Invalid input for \'email\'");
                await next(context);
                return;
            }

            if (passwords.Count == 0)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync($"Invalid input for \'password\'");
                await next(context);
                return;
            }

            var email = emails[0];
            var password = passwords[0];

            if (email == "admin@example.com" && password == "admin1234")
            {
                context.Response.StatusCode = StatusCodes.Status200OK;
                await context.Response.WriteAsync("Successful login");
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Invalid login");
            }

            await next(context);
        }
    }

    public static class AuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyAuthenticationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseWhen(
                context => context.Request.Path.Value == "/" && context.Request.Method == "POST",
                app =>
                {
                    app.UseMiddleware<MyAuthenticationMiddleware>();
                }
                );
        }
    }
}
