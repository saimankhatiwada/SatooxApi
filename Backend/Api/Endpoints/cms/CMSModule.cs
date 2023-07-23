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
    public CMSModule(IRepository<NormalUser> normalUserRepository, IRepository<AdminUser> adminUserRepository, IRepository<Blog> blogRepository) : base("/CMS")
    {
        _normalUserRepository = normalUserRepository;
        _adminUserRepository = adminUserRepository;
        _blogRepository = blogRepository;
        this.RequireAuthorization();
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/user", async () => {

            return Results.Ok(await _normalUserRepository.GetAllAsync());
        });

        app.MapGet("/user/{id:Guid}", async (Guid id) => {

            return Results.Ok(await _normalUserRepository.GetByIdAsync(id));
        });

        app.MapPatch("/user/{id:Guid}/changepasswrod", async (Guid id, ChangePassword password) => {
            
            NormalUser? user = await _normalUserRepository.GetByIdAsync(id);

            if (user is null)
            {
                Failure Errorresponse = new Failure();
                Errorresponse.ErrorMessage = ErrorsValueMapping.GetStringValue(ErrorDefinations.UserNotFound);
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
                Failure Errorresponse = new Failure();
                Errorresponse.ErrorMessage = ErrorsValueMapping.GetStringValue(ErrorDefinations.UserNotFound);
                return Results.BadRequest(Errorresponse);
            }

            _normalUserRepository.Delete(user);
            await _normalUserRepository.SaveChangesAsync();

            return Results.Ok();
        });

        app.MapGet("/blog/get", async () => {

            return Results.Ok(await _blogRepository.GetAllAsync());
        });

        app.MapPost("/blog/Create", async (BlogRequest blogrequest) => {

            AdminUser? admin = await _adminUserRepository.GetByIdAsync(blogrequest.AdminId);

            if (admin is null)
            {
                Failure Errorresponse = new Failure();
                Errorresponse.ErrorMessage = ErrorsValueMapping.GetStringValue(ErrorDefinations.UserNotFound);
                return Results.BadRequest(Errorresponse);
            }

            Blog blog = new Blog{

                Tittle = blogrequest.Tittle,
                Description = blogrequest.Description,
                Author = $"{admin.FirstName} {admin.LaastName}",
                Published = DateOnly.FromDateTime(DateTime.UtcNow.Date),
                Modified = DateOnly.FromDateTime(DateTime.UtcNow.Date),
                AdminId = blogrequest.AdminId
            };

            await _blogRepository.InsertAsync(blog);
            await _blogRepository.SaveChangesAsync();

            return Results.Ok();
        });
    }
}
