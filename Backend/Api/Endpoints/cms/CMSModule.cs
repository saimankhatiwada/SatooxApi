using System.Net.Http.Headers;
using Api.Services.IService;
using Api.utils;
using BussinessLogic.IRepositories;
using Carter;
using Contracts.HttpResults;
using Contracts.SafteyFocused;
using Data.Blogs;
using Data.Users.Admin;
using Data.Users.Normal;

namespace Api.Endpoints.cms;

public class CMSModule : CarterModule
{
    private readonly IRepository<NormalUser> _normalUserRepository;
    private readonly IRepository<AdminUser> _adminUserRepository;
    private readonly IRepository<Blog> _blogRepository;
    private readonly IFileUploadService _fileUploadService;
    private readonly IHostEnvironment _hostEnviroment;
    public CMSModule(IRepository<NormalUser> normalUserRepository, IRepository<AdminUser> adminUserRepository, IRepository<Blog> blogRepository, 
                    IFileUploadService fileUploadService, IHostEnvironment hostEnvironment) : base("/CMS")
    {
        _normalUserRepository = normalUserRepository;
        _adminUserRepository = adminUserRepository;
        _blogRepository = blogRepository;
        _fileUploadService = fileUploadService;
        _hostEnviroment = hostEnvironment;
        this.RequireAuthorization();
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/admin/{id:Guid}/uploadAdminImage", async (Guid id, IFormFile file) => {

            AdminUser? admin = await _adminUserRepository.GetByIdAsync(id);
            if (admin is null)
            {
                Failure Errorresponse = new Failure{
                    Code = StatusCodes.Status400BadRequest,
                    ErrorMessage = ErrorsValueMapping.GetStringValue(ErrorDefinations.InvalidUserId)
                };
                return Results.BadRequest(Errorresponse);
            }
            List<string> uploadDetail = await _fileUploadService.UploadFile(file, "Admin");
            admin.ImageName = uploadDetail[0];
            admin.ImagePath = uploadDetail[1];
            _adminUserRepository.Update(admin);
            await _adminUserRepository.SaveChangesAsync();
            UploadSuccess success = new UploadSuccess {
                Status = StatusCodes.Status200OK
            };
            return  Results.Ok(success);
        });

        app.MapPut("/admin/{id:Guid}/update/adminImage", async (Guid id, IFormFile file) =>
        {
            AdminUser? admin = await _adminUserRepository.GetByIdAsync(id);
            if (admin is null)
            {
               Failure Errorresponse = new Failure{
                    Code = StatusCodes.Status400BadRequest,
                    ErrorMessage = ErrorsValueMapping.GetStringValue(ErrorDefinations.InvalidUserId)
                };
                return Results.BadRequest(Errorresponse);
            }
            List<string> uploadDetail = await _fileUploadService.UploadFile(file, "Admin");
            if(! _fileUploadService.DeleteFile(admin.ImagePath))
            {
                Failure Errorresponse = new Failure();
                Errorresponse.ErrorMessage = ErrorsValueMapping.GetStringValue(ErrorDefinations.ImageNotFound);
                return Results.BadRequest(Errorresponse);
            }
            admin.ImageName = uploadDetail[0];
            admin.ImagePath = uploadDetail[1];
            _adminUserRepository.Update(admin);
            await _adminUserRepository.SaveChangesAsync();
            UploadSuccess success = new UploadSuccess {
                Status = StatusCodes.Status200OK
            };
            return  Results.Ok(success);
        });

        app.MapGet("/admin/{id:Guid}/getAdminImage",async (Guid id) =>
        {
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
                string inline = mimeType == ".png" ? $"{admin.ImageName}" : $"{admin.ImageName}";
                return Results.File(image, mimeType, new ContentDispositionHeaderValue(inline).ToString());
            }
            return Results.NoContent();
        });


        app.MapGet("/user", async () => {

            IEnumerable<NormalUser> users = await _normalUserRepository.GetAllAsync();
            if(!users.Any())
            {
                Failure Errorresponse = new Failure{
                    Code = StatusCodes.Status204NoContent,
                    ErrorMessage = ErrorsValueMapping.GetStringValue(ErrorDefinations.UserNotFound)
                };
                return Results.BadRequest(Errorresponse);
            }
            return Results.Ok(users);
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
            return Results.Ok(user);
        });

        app.MapPatch("/user/{id:Guid}/changepasswrod", async (Guid id, ChangePassword password) => {
            
            NormalUser? user = await _normalUserRepository.GetByIdAsync(id);
            if (user is null)
            {
                Failure Errorresponse = new Failure{
                    Code = StatusCodes.Status400BadRequest,
                    ErrorMessage = ErrorsValueMapping.GetStringValue(ErrorDefinations.InvalidUserId)
                };
                return Results.BadRequest(Errorresponse);
            }
            user.Password = BCrypt.Net.BCrypt.HashPassword(password.newpassword);
            _normalUserRepository.Update(user);
            await _normalUserRepository.SaveChangesAsync();

            return Results.Ok();
        });

        app.MapDelete("/user/{id:Guid}/delete", async (Guid id) => {
            
            NormalUser? user = await _normalUserRepository.GetByIdAsync(id);
            if (user is null)
            {
                Failure Errorresponse = new Failure{
                    Code = StatusCodes.Status400BadRequest,
                    ErrorMessage = ErrorsValueMapping.GetStringValue(ErrorDefinations.InvalidUserId)
                };
                return Results.BadRequest(Errorresponse);
            }
            _normalUserRepository.Delete(user);
            await _normalUserRepository.SaveChangesAsync();

            return Results.Ok();
        });

        app.MapGet("/user/{id:Guid}/getProfileImage",async (Guid id) =>
        {
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
                string inline = mimeType == ".png" ? $"{user.ImageName}" : $"{user.ImageName}";
                return Results.File(image, mimeType, new ContentDispositionHeaderValue(inline).ToString());
            }

            return Results.NoContent();
        });

        app.MapGet("/blog", async () => {

            IEnumerable<Blog> blogs = await _blogRepository.GetAllAsync();
            if(!blogs.Any())
            {
                Failure Errorresponse = new Failure{
                    Code = StatusCodes.Status204NoContent,
                    ErrorMessage = ErrorsValueMapping.GetStringValue(ErrorDefinations.BlogNotFound)
                };
                return Results.BadRequest(Errorresponse);
            }
            return Results.Ok(blogs);
        });

        app.MapGet("/blog/{id:Guid}/get", async (Guid id) => {

            Blog? blog = await _blogRepository.GetByIdAsync(id);
            if (blog is null)
            {
                Failure Errorresponse = new Failure{
                    Code = StatusCodes.Status400BadRequest,
                    ErrorMessage = ErrorsValueMapping.GetStringValue(ErrorDefinations.InvalidBlogId)
                };
                return Results.BadRequest(Errorresponse);
            }
            return Results.Ok(blog);
        });

        app.MapPost("/blog/Create", async (BlogRequest blogrequest) => {

            AdminUser? admin = await _adminUserRepository.GetByIdAsync(blogrequest.AdminId);
            if (admin is null)
            {
                Failure Errorresponse = new Failure{
                    Code = StatusCodes.Status400BadRequest,
                    ErrorMessage = ErrorsValueMapping.GetStringValue(ErrorDefinations.UserNotFound)
                };
                return Results.BadRequest(Errorresponse);
            }
            Blog blog = new Blog{

                Tittle = blogrequest.Tittle,
                Description = blogrequest.Description,
                Author = $"{admin.FirstName} {admin.LaastName}",
                ImageName = "N/A",
                ImagePath = "N/A",
                Published = DateOnly.FromDateTime(DateTime.UtcNow.Date),
                Modified = DateOnly.FromDateTime(DateTime.UtcNow.Date),
                AdminId = blogrequest.AdminId
            };
            await _blogRepository.InsertAsync(blog);
            await _blogRepository.SaveChangesAsync();
            return Results.Ok();
        });

        app.MapPut("/blog/{id:Guid}/update", async (Guid id, BlogRequest blogRequest) => {

            Blog? blog = await _blogRepository.GetByIdAsync(id);
            AdminUser? admin = await _adminUserRepository.GetByIdAsync(blogRequest.AdminId);
            if (blog is null)
            {
                Failure Errorresponse = new Failure{
                    Code = StatusCodes.Status400BadRequest,
                    ErrorMessage = ErrorsValueMapping.GetStringValue(ErrorDefinations.InvalidBlogId)
                };
                return Results.BadRequest(Errorresponse);
            }
            if (admin is null)
            {
                Failure Errorresponse = new Failure{
                    Code = StatusCodes.Status400BadRequest,
                    ErrorMessage = ErrorsValueMapping.GetStringValue(ErrorDefinations.UserNotFound)
                };
                return Results.BadRequest(Errorresponse);
            }
            blog.Tittle = blogRequest.Tittle;
            blog.Description = blogRequest.Description;
            blog.Author = $"{admin.FirstName} {admin.LaastName}";
            blog.Modified = DateOnly.FromDateTime(DateTime.UtcNow.Date);
            blog.AdminId = blogRequest.AdminId;
            _blogRepository.Update(blog);
            await _blogRepository.SaveChangesAsync();
            return Results.Ok();
        });

        app.MapDelete("/blog/{id:Guid}/delete", async (Guid id) => {
            
            Blog? blog = await _blogRepository.GetByIdAsync(id);
            if (blog is null)
            {
                Failure Errorresponse = new Failure{
                    Code = StatusCodes.Status400BadRequest,
                    ErrorMessage = ErrorsValueMapping.GetStringValue(ErrorDefinations.InvalidBlogId)
                };
                return Results.BadRequest(Errorresponse);
            }
            _blogRepository.Delete(blog);
            await _blogRepository.SaveChangesAsync();

            return Results.Ok();
        });

        app.MapPost("/blog/{id:Guid}/uploadBlogImage", async (Guid id, IFormFile file) => {

            Blog? blog = await _blogRepository.GetByIdAsync(id);
            if (blog is null)
            {
                Failure Errorresponse = new Failure{
                    Code = StatusCodes.Status400BadRequest,
                    ErrorMessage = ErrorsValueMapping.GetStringValue(ErrorDefinations.InvalidBlogId)
                };
                return Results.BadRequest(Errorresponse);
            }
            List<string> uploadDetail = await _fileUploadService.UploadFile(file, "Blog");
            blog.ImageName = uploadDetail[0];
            blog.ImagePath = uploadDetail[1];
            _blogRepository.Update(blog);
            await _blogRepository.SaveChangesAsync();
            UploadSuccess success = new UploadSuccess {
                Status = StatusCodes.Status200OK
            };
            return  Results.Ok(success);
        });

        app.MapPut("/blog/{id:Guid}/update/blogImage", async (Guid id, IFormFile file) =>
        {
            Blog? blog = await _blogRepository.GetByIdAsync(id);
            if (blog is null)
            {
                Failure Errorresponse = new Failure{
                    Code = StatusCodes.Status400BadRequest,
                    ErrorMessage = ErrorsValueMapping.GetStringValue(ErrorDefinations.InvalidBlogId)
                };
                return Results.BadRequest(Errorresponse);
            }
            List<string> uploadDetail = await _fileUploadService.UploadFile(file, "Blog");
            if(! _fileUploadService.DeleteFile(blog.ImagePath))
            {
                Failure Errorresponse = new Failure{
                    Code = StatusCodes.Status204NoContent,
                    ErrorMessage = ErrorsValueMapping.GetStringValue(ErrorDefinations.ImageNotFound)
                };
                return Results.BadRequest(Errorresponse);
            }
            blog.ImageName = uploadDetail[0];
            blog.ImagePath = uploadDetail[1];
            _blogRepository.Update(blog);
            await _blogRepository.SaveChangesAsync();
            UploadSuccess success = new UploadSuccess {
                Status = StatusCodes.Status200OK
            };
            return  Results.Ok(success);
        });

        app.MapGet("/blog/{id:Guid}/getBlogImage",async (Guid id) =>
        {
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
                string inline = mimeType == ".png" ? $"{blog.ImageName}" : $"{blog.ImageName}";
                return Results.File(image, mimeType, new ContentDispositionHeaderValue(inline).ToString());
            }
            return Results.NoContent();
        });
    }
}
