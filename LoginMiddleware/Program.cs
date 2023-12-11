using System.Runtime.Versioning;

namespace LoginMiddleware
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddTransient<MyAuthenticationMiddleware>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseMyAuthenticationMiddleware();

            app.Run();
        }
    }
}