using Microsoft.AspNetCore.Http;
using MiddlewareExample.CustomMiddleware;

namespace MiddlewareExample;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.        
        //builder.Services.AddTransient<MyCustomMiddleware>();

        var app = builder.Build();


        /*app.Use(async (HttpContext context, RequestDelegate next) =>
        {
            await context.Response.WriteAsync($"Use middleware Starts\n");
            await next(context);
            await context.Response.WriteAsync($"Use middleware Ends\n");
        });*/

        app.UseMyCustomMiddleware();
        app.UseHelloCustomMiddleware();

        // Configure the HTTP request pipeline.
        app.Run(async context =>
        {
            await context.Response.WriteAsync($"Hello World!\n");
        });

        app.Run(async context =>
        {
            await context.Response.WriteAsync($"Hello World!\n");
        });

        app.UseWhen(
            context =>
                context.Request.Query.ContainsKey("first-name") && context.Request.Query.ContainsKey("last-name"),
            app =>
            {
                app.UseMiddleware<HelloCustomMiddleware>();
            });

        app.Run();
    }
}
