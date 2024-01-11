using _1.RoutingExample.CustomConstraint;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRouting(opt => opt.ConstraintMap.Add("months", typeof(MonthsCustomConstraint)));

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.Use(async (context, next) =>
{
    var endpoint = context.GetEndpoint();
    if (endpoint != null)
        await context.Response.WriteAsync($"Hello from 1st delegate: {endpoint.DisplayName}\n");
    await next(context);
});


app.UseEndpoints(endpoints =>
{
    endpoints.Map("files/{filename}.{extension:maxlength(3)}", async context =>
    {
        var filename = context.Request.RouteValues["filename"]?.ToString();
        var extension = context.Request.RouteValues["extension"]?.ToString();
        await context.Response.WriteAsync($"Request received for file: {filename} - {extension}");
    });

    endpoints.MapGet("employees/profile/{employeeName:length(4,7):alpha=rafael}", async context =>
    {
        string? employeeName = context.Request.RouteValues["employeeName"]?.ToString();
        await context.Response.WriteAsync($"Request received for employee: {employeeName}");
    });

    //Eg: daily-digest-report/{reportdate}
    endpoints.Map("daily-digest-report/{reportdate:datetime}", async context =>
    {
        var reportDate = Convert.ToDateTime(context.Request.RouteValues["reportdate"]).ToUniversalTime();
        await context.Response.WriteAsync($"Request received for report date: {reportDate}");
    });

    //Eg: cities/cityid
    endpoints.Map("cities/{cityid:guid}", async context =>
    {
        var cityId = Guid.Empty;
        var cityIdRaw = context.Request.RouteValues["cityid"];
        if (cityIdRaw != null)
        {
            cityId = Guid.Parse(cityIdRaw.ToString()!);
        }
        await context.Response.WriteAsync($"Request received for city id: {cityId}");
    });

    //Eg: sales-report/{reportYear}/{reportMonth}
    endpoints.Map("sales-report/{reportYear:min(1900)}/{month:months}", async context =>
    {
        var reportYear = Convert.ToInt32(context.Request.RouteValues["reportYear"]);
        var reportMonth = context.Request.RouteValues["month"]?.ToString();
        await context.Response.WriteAsync($"Request received for report year: {reportYear} and month: {reportMonth}");
    });

    //Eg: files
    endpoints.Map("/", async context =>
    {
        await context.Response.WriteAsync($"Hello World!");
    });
});

app.Run(context => context.Response.WriteAsync($"Request made at {context.Request.Path}"));

app.Run();