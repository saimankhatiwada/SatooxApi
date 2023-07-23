using Serilog;
using DotNetEnv;
using Utils.Definations;
using Api.utils;
using Carter;
using Api.DependencyInjections;

Env.Load();

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
{
    builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
    if ( EnvValueGetter.Get<bool>(EnvVarriableNamesMapping.GetStringValue(EnvVarriableNames.Development)))
    {
        builder.Services.AddDevelopmentHelperServices();
    }
    builder.Services.AddSatooxApiServices();
}
Console.WriteLine(Directory.GetCurrentDirectory());
WebApplication app = builder.Build();
{
    
    app.UseSerilogRequestLogging();

    if ( EnvValueGetter.Get<bool>(EnvVarriableNamesMapping.GetStringValue(EnvVarriableNames.Development))) // just in development
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Saiman API v1");
            c.RoutePrefix= String.Empty;
        });
    }
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapCarter();
    app.Run();
}

