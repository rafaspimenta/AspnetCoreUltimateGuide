using Microsoft.AspNetCore.Builder;
using System;

namespace _2.CountryDetails;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var app = builder.Build();
        app.UseRouting();

        var countries = new Dictionary<int, string>
        {
            { 1, "United States" },
            { 2, "Canada" },
            { 3, "United Kingdom" },
            { 4, "India" },
            { 5, "Japan" }
        };

        app.UseEndpoints(endpoints =>
        {
            endpoints.Map("/countries", async context =>
            {
                await context.Response.WriteAsync($"Countries: {string.Join(", ", countries.Values)}");
            });

            endpoints.Map("/countries/{countryID}", async context =>
            {
                var countryID = context.Request.RouteValues["countryID"]?.ToString();
                if (int.TryParse(countryID, out int id))
                {
                    if (countries.TryGetValue(id, out string? value))
                    {
                        var country = value;
                        context.Response.StatusCode = 200;
                        await context.Response.WriteAsync($"Country: {country}");
                    }
                    else
                    {
                        context.Response.StatusCode = 404;
                        await context.Response.WriteAsync("Country not found");
                    }
                }
                else
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("Invalid country ID");
                }
            });
        });

        app.Run();
    }

}
