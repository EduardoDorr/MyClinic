using Serilog;

using MyClinic.API.Configurations;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureSerilog();

try
{
    Log.Information("Application starting.");
    // Add services to the container.
    builder.ConfigureServices();

    var app = builder.Build();
    // Configure the HTTP request pipeline.
    app.ConfigureApplication();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application has found an error in runtime.");
}
finally
{
    Log.CloseAndFlush();
}