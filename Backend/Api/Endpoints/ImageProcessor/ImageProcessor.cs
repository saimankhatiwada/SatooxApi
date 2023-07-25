using System.Net.Http.Headers;
using Api.utils;
using BussinessLogic.IRepositories;
using Carter;
using Contracts.HttpResults;
using Data.Blogs;
using Data.Users.Admin;
using Data.Users.Normal;

namespace Api.Endpoints.ImageProcessor;

public class ImageProcessor : CarterModule
{
    private readonly IRepository<NormalUser> _normalUserRepository;
    private readonly IRepository<AdminUser> _adminUserRepository;
    private readonly IRepository<Blog> _blogRepository;
    private readonly IHostEnvironment _hostEnviroment;
    public ImageProcessor(IRepository<AdminUser> adminUserRepository, IRepository<NormalUser> normalUserRepository,
                         IRepository<Blog> blogRepository, IHostEnvironment hostEnviroment) : base("/Image")
    {
        _adminUserRepository = adminUserRepository;
        _normalUserRepository = normalUserRepository;
        _blogRepository = blogRepository;
        _hostEnviroment = hostEnviroment;
        this.RequireAuthorization();
    }
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/admin/{id:Guid}", async (Guid id) => {

            AdminUser? admin = await _adminUserRepository.GetByIdAsync(id);
            if (admin is null)
            {
               Failure Errorresponse = new Failure{
                    Code = StatusCodes.Status400BadRequest,
                    ErrorMessage = ErrorsValueMapping.GetStringValue(ErrorDefinations.InvalidUserId)
                };
                return Results.BadRequest(Errorresponse);
            }
            if (File.Exists(_hostEnviroment.ContentRootPath + admin.ImagePath))
            {
                byte[] image = await File.ReadAllBytesAsync(_hostEnviroment.ContentRootPath + admin.ImagePath);
                string mimeType = Path.GetExtension(admin.ImagePath).ToLowerInvariant();
                string contentType = mimeType == ".png" ? "image/png" : "image/jpg";
                return Results.File(image, contentType);
            }
            return Results.NoContent();
        });

        app.MapGet("/user/{id:Guid}", async (Guid id) => {

            NormalUser? user = await _normalUserRepository.GetByIdAsync(id);
            if (user is null)
            {
                Failure Errorresponse = new Failure{
                    Code = StatusCodes.Status400BadRequest,
                    ErrorMessage = ErrorsValueMapping.GetStringValue(ErrorDefinations.InvalidUserId)
                };
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

        app.MapGet("/blog/{id:Guid}", async (Guid id) => {

            Blog? blog = await _blogRepository.GetByIdAsync(id);
            if (blog is null)
            {
                Failure Errorresponse = new Failure{
                    Code = StatusCodes.Status400BadRequest,
                    ErrorMessage = ErrorsValueMapping.GetStringValue(ErrorDefinations.InvalidBlogId)
                };
                return Results.BadRequest(Errorresponse);
            }
            if (File.Exists(_hostEnviroment.ContentRootPath + blog.ImagePath))
            {
                byte[] image = await File.ReadAllBytesAsync(_hostEnviroment.ContentRootPath + blog.ImagePath);
                string mimeType = Path.GetExtension(blog.ImagePath).ToLowerInvariant();
                string contentType = mimeType == ".png" ? "image/png" : "image/jpg";
                return Results.File(image, contentType);
            }
            return Results.NoContent();
        });
        
    }
}
