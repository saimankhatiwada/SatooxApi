using System.Net.Http.Headers;
using Api.Services.IService;
using Api.utils;
using BussinessLogic.IRepositories;
using Carter;
using Contracts.HttpResults;
using Data.Users.Normal;

namespace Api.Endpoints.KYC;

public class KYCModule : CarterModule
{
    private readonly IRepository<NormalUser> _normalUserRepository;
    private readonly IFileUploadService _fileUploadService;
    private readonly IHostEnvironment _hostEnviroment;

    public KYCModule(IRepository<NormalUser> normalUserRepository, IFileUploadService fileUploadService, IHostEnvironment hostEnviroment) : base("/KYC")
    {
        _normalUserRepository = normalUserRepository;
        _fileUploadService = fileUploadService;
        _hostEnviroment = hostEnviroment;
        this.RequireAuthorization();
    }
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/user/{id:Guid}/uploadProfileImage", async (Guid id, IFormFile file) =>
        {
            NormalUser? user = await _normalUserRepository.GetByIdAsync(id);
            if (user is null)
            {
                Failure Errorresponse = new Failure();
                Errorresponse.ErrorMessage = ErrorsValueMapping.GetStringValue(ErrorDefinations.InvalidUserId);
                return Results.BadRequest(Errorresponse);
            }
            List<string> uploadDetail = await _fileUploadService.UploadFile(file, "NormalUser");
            user.ImageName = uploadDetail[0];
            user.ImagePath = uploadDetail[1];
            _normalUserRepository.Update(user);
            await _normalUserRepository.SaveChangesAsync();
            UploadSuccess success = new UploadSuccess {
                Status = StatusCodes.Status200OK
            };
            return  Results.Ok(success);
        });

        app.MapPut("/user/{id:Guid}/update/profileImage", async (Guid id, IFormFile file) =>
        {
            NormalUser? user = await _normalUserRepository.GetByIdAsync(id);
            if (user is null)
            {
                Failure Errorresponse = new Failure();
                Errorresponse.ErrorMessage = ErrorsValueMapping.GetStringValue(ErrorDefinations.InvalidUserId);
                return Results.BadRequest(Errorresponse);
            }
            List<string> uploadDetail = await _fileUploadService.UploadFile(file, "NormalUser");
            if(! _fileUploadService.DeleteFile(user.ImagePath))
            {
                Failure Errorresponse = new Failure();
                Errorresponse.ErrorMessage = ErrorsValueMapping.GetStringValue(ErrorDefinations.ImageNotFound);
                return Results.BadRequest(Errorresponse);
            }
            user.ImageName = uploadDetail[0];
            user.ImagePath = uploadDetail[1];
            _normalUserRepository.Update(user);
            await _normalUserRepository.SaveChangesAsync();
            UploadSuccess success = new UploadSuccess {
                Status = StatusCodes.Status200OK
            };
            return  Results.Ok(success);
        });

        app.MapGet("/user/{id:Guid}/getProfileImage",async (Guid id) =>
        {
            NormalUser? user = await _normalUserRepository.GetByIdAsync(id);
            if (user is null)
            {
                Failure Errorresponse = new Failure();
                Errorresponse.ErrorMessage = ErrorsValueMapping.GetStringValue(ErrorDefinations.InvalidUserId);
                return Results.BadRequest(Errorresponse);
            }
            if (File.Exists(_hostEnviroment.ContentRootPath + user.ImagePath))
            {
                byte[] image = await File.ReadAllBytesAsync(_hostEnviroment.ContentRootPath + user.ImagePath);
                string mimeType = Path.GetExtension(user.ImagePath).ToLowerInvariant();
                string inline = mimeType == ".png" ? $"{user.ImageName}" : $"{user.ImageName}";
                return Results.File(image, mimeType, new ContentDispositionHeaderValue(inline).ToString());
            }

            return Results.NoContent();
        });
    }
}
