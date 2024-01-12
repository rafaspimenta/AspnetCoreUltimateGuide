using _01_DIExampleServices;
using _01_ServiceContracts;

namespace _01_DIExample;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllersWithViews();

        builder.Services.AddScoped(typeof(ICitiesService), typeof(CitiesService));

        var app = builder.Build();
        app.UseStaticFiles();
        //app.UseRouting();
        app.MapControllers();
        app.Run();
    }
}
