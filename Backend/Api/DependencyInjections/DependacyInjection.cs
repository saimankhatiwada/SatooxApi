using System.Text;
using Api.Services.IService;
using Api.Services.Service;
using Api.utils;
using BussinessLogic.IRepositories;
using BussinessLogic.Logic.Authentication.Definations;
using BussinessLogic.Logic.Authentication.IDefinations;
using BussinessLogic.Logic.Security.JWT.ITokenGenerator;
using BussinessLogic.Logic.Security.JWT.TokenGenerator;
using BussinessLogic.Logic.Security.Service.DateTime;
using BussinessLogic.Logic.Security.Service.IService;
using BussinessLogic.Repositories;
using Carter;
using Data.DatabaseContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Utils.Definations;

namespace Api.DependencyInjections;

public static class DependacyInjection
{
    public static IServiceCollection AddSatooxApiServices(this IServiceCollection service)
    {
        service.AddSecurityTokenService();
        service.AddAuthenticationLogic();
        service.AddRepository();
        service.AddSaimanDatabase();
        service.AddScoped<IDataSeeder, DataSeeder>();
        service.AddFileUploadService();
        service.AddCustomAuthentication();
        service.AddCustomEndpointExplorere();
        service.AddCustomeCORSPolicy();
        return service;
    }
    public static IServiceCollection AddCustomEndpointExplorere(this IServiceCollection service)
    {
        // service.AddScoped<Carter.ICarterModule, Api.Endpoints.cms.CMSModule>(); // Change to scoped or transient if needed.
        // service.AddScoped<Carter.ICarterModule, Api.Endpoints.Authentication.AuthenticationModule>();
        service.AddCarter();
        return service;
    }
    public static IServiceCollection AddSaimanDatabase(this IServiceCollection service)
    {
        service.AddDbContext<ApplicationDbContext>(options => options.UseMySql(EnvValueGetter.Get<string>(EnvVarriableNamesMapping.GetStringValue(EnvVarriableNames.DataBaseConnection)),
                                                ServerVersion.AutoDetect(EnvValueGetter.Get<string>(EnvVarriableNamesMapping.GetStringValue(EnvVarriableNames.DataBaseConnection)))));

        return service;
    }
    public static IServiceCollection AddRepository(this IServiceCollection service)
    {
        service.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        return service;
    }

    public static IServiceCollection AddAuthenticationLogic(this IServiceCollection service)
    {
        service.AddScoped<IUserAuthentication, UserAuthentication>();
        service.AddScoped<IAdminAuthentication, AdminAuthentication>();
        return service;
    }

    public static IServiceCollection AddSecurityTokenService(this IServiceCollection service)
    {
        service.AddSingleton<IJWTTokenGenerator, JWTTokenGenerator>();
        service.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        return service;
    }

    public static IServiceCollection AddCustomAuthentication(this IServiceCollection service)
    {
        service.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters{

                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = EnvValueGetter.Get<string>(EnvVarriableNamesMapping.GetStringValue(EnvVarriableNames.TokenIssuer)),
                    ValidAudience = EnvValueGetter.Get<string>(EnvVarriableNamesMapping.GetStringValue(EnvVarriableNames.TokenAudience)),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(EnvValueGetter.Get<string>(EnvVarriableNamesMapping.GetStringValue(EnvVarriableNames.TokenSecretNormal))))

            });

        service.AddAuthorization();
        return service;
    }


    public static IServiceCollection AddFileUploadService(this IServiceCollection service)
    {
        service.AddScoped<IFileUploadService,FileUploadService>();
        return service;
    }

    public static IServiceCollection AddCustomeCORSPolicy(this IServiceCollection service)
    {
        service.AddCors(o => o.AddPolicy("SatooxHolders", builder =>
        {
            builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        }));

        return service;
    }

}
