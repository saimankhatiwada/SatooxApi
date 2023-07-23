using System.Net.Http.Headers;
using Api.utils;
using BussinessLogic.IRepositories;
using Carter;
using Contracts.HttpResults;
using Data.Users.Normal;

namespace Api.Endpoints.ImageProcessor;

public class ImageProcessor : CarterModule
{
    private readonly IRepository<NormalUser> _normalUserRepository;

    private readonly IHostEnvironment _hostEnviroment;
    public ImageProcessor(IRepository<NormalUser> normalUserRepository, IHostEnvironment hostEnviroment) : base("/Image")
    {
        _normalUserRepository = normalUserRepository;
        _hostEnviroment = hostEnviroment;
    }
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/user/{id:Guid}", async (Guid id) => {

            NormalUser? user = await _normalUserRepository.GetByIdAsync(id);

            if (user is null)
            {
                Failure Errorresponse = new Failure();
                Errorresponse.ErrorMessage = ErrorsValueMapping.GetStringValue(ErrorDefinations.UserNotFound);
                return Results.BadRequest(Errorresponse);
            }

            if (File.Exists(_hostEnviroment.ContentRootPath + user.ImagePath))
            {
                byte[] image = await File.ReadAllBytesAsync(_hostEnviroment.ContentRootPath + user.ImagePath);
                string mimeType = Path.GetExtension(user.ImagePath).ToLowerInvariant();
                string contentType = mimeType == ".png" ? "image/png" : "image/jpg";
                return Results.File(image, contentType);
            }

            return Results.NoContent();
        });
    }
}
