using Serilog;
using DotNetEnv;
using Carter;
using Api.DependencyInjections;
using Api.Services.IService;

Env.Load();

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
{
    builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
    builder.Services.AddSatooxApiServices();
}

WebApplication app = builder.Build();
{

    app.UseSerilogRequestLogging();
    app.UseHttpsRedirection();
    app.UseCors("SatooxHolders");
    app.UseRouting();
    SeedDatabase();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapCarter();
    app.Run();

    void SeedDatabase()
    {
        using (var scope = app.Services.CreateScope())
        {
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDataSeeder>();
            dbInitializer.Initialize();
        }
    }
}

