namespace EnvironmentsExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            /*if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }*/

            app.UseStaticFiles();
            app.MapControllers();

            app.Run();
        }
    }
}
