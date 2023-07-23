using Api.utils;
using BussinessLogic.IRepositories;
using BussinessLogic.Logic.Authentication.IDefinations;
using BussinessLogic.Logic.Security.JWT.ITokenGenerator;
using Carter;
using Contracts.Authentications.Login;
using Contracts.Authentications.Registration;
using Contracts.HttpResults;
using Data.Users.Admin;
using Data.Users.Normal;
using Utils.Definations;



namespace Api.Endpoints.Authentication;

public class AuthenticationModule : CarterModule
{
    private readonly IRepository<NormalUser> _normalUserRepository;
    private readonly IRepository<AdminUser> _adminUserRepository;
    private readonly IJWTTokenGenerator _jwtTokenGenerator;
    private readonly IUserAuthentication _userAuthentication;
    private readonly IAdminAuthentication _adminAuthentication;
    
    public AuthenticationModule(IRepository<NormalUser> normalUserRepository, IRepository<AdminUser> adminUserRepository,
                                IJWTTokenGenerator jwtTokenGenerator, IUserAuthentication userAuthentication,
                                IAdminAuthentication adminAuthentication) : base("/Authentication")
    {
        _normalUserRepository = normalUserRepository;
        _adminUserRepository = adminUserRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _userAuthentication = userAuthentication;
        _adminAuthentication = adminAuthentication;
    }
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/admin/login", async (Logins loginrequest) => {

            if(!await _adminAuthentication.CheckAdminByEmailAsync(loginrequest.Email))
            {
                Failure Errorresponse = new Failure();
                Errorresponse.ErrorMessage = ErrorsValueMapping.GetStringValue(ErrorDefinations.UserNotFound);
                return Results.BadRequest(Errorresponse);
            }

            AdminUser loginUser = new AdminUser();

            loginUser = await _adminAuthentication.GetAdminByEmailAsync(loginrequest.Email);

            if(!BCrypt.Net.BCrypt.Verify(loginrequest.Password, loginUser.Password))
            {
                Failure Errorresponse = new Failure();
                Errorresponse.ErrorMessage = ErrorsValueMapping.GetStringValue(ErrorDefinations.UserInvalidPassword);
                return Results.BadRequest(Errorresponse);
            }


            string token = _jwtTokenGenerator.GenerateTokenSystemUser(loginUser,
                        EnvValueGetter.Get<string>(EnvVarriableNamesMapping.GetStringValue(EnvVarriableNames.TokenSecretNormal)),
                        EnvValueGetter.Get<string>(EnvVarriableNamesMapping.GetStringValue(EnvVarriableNames.TokenIssuer)),
                        EnvValueGetter.Get<string>(EnvVarriableNamesMapping.GetStringValue(EnvVarriableNames.TokenAudience)),
                        EnvValueGetter.Get<int>(EnvVarriableNamesMapping.GetStringValue(EnvVarriableNames.TokenExpiryInMinutes)));

            
            Success sucessResponse = new Success{
                Email = loginUser.Email,
                Role = loginUser.Role,
                Token = token
            };

            return Results.Ok(sucessResponse);
        });


        app.MapPost("/admin/register", async (Registrations registerrequest) => {

            if(await _adminAuthentication.CheckAdminByEmailAsync(registerrequest.Email))
            {
                Failure Errorresponse = new Failure();
                Errorresponse.ErrorMessage = ErrorsValueMapping.GetStringValue(ErrorDefinations.UserExists);
                return Results.BadRequest(Errorresponse);
            }

            AdminUser registeruser = new AdminUser {
                FirstName = registerrequest.FirstName,
                LaastName = registerrequest.LastName,
                Email = registerrequest.Email,
                ImageName = "N/A",
                ImagePath = "N/A",
                Password = BCrypt.Net.BCrypt.HashPassword(registerrequest.Password),
                Role = EnvValueGetter.Get<string>(EnvVarriableNamesMapping.GetStringValue(EnvVarriableNames.UserAdmin)),
                IsActive = true
            };

            await _adminUserRepository.InsertAsync(registeruser);
            await _adminUserRepository.SaveChangesAsync();

            string token = _jwtTokenGenerator.GenerateTokenSystemUser(registeruser,
                        EnvValueGetter.Get<string>(EnvVarriableNamesMapping.GetStringValue(EnvVarriableNames.TokenSecretNormal)),
                        EnvValueGetter.Get<string>(EnvVarriableNamesMapping.GetStringValue(EnvVarriableNames.TokenIssuer)),
                        EnvValueGetter.Get<string>(EnvVarriableNamesMapping.GetStringValue(EnvVarriableNames.TokenAudience)),
                        EnvValueGetter.Get<int>(EnvVarriableNamesMapping.GetStringValue(EnvVarriableNames.TokenExpiryInMinutes)));

            Success sucessResponse = new Success{
                Email = registeruser.Email,
                Role = registeruser.Role,
                Token = token
            };

            return Results.Ok(sucessResponse);
        });


        app.MapPost("/user/login", async (Logins loginrequest) => {

            if(!await _userAuthentication.CheckUserByEmailAsync(loginrequest.Email))
            {
                Failure Errorresponse = new Failure();
                Errorresponse.ErrorMessage = ErrorsValueMapping.GetStringValue(ErrorDefinations.UserNotFound);
                return Results.BadRequest(Errorresponse);
            }

            NormalUser loginUser = new NormalUser();

            loginUser = await _userAuthentication.GetUserByEmailAsync(loginrequest.Email);

            if(!BCrypt.Net.BCrypt.Verify(loginrequest.Password, loginUser.Password))
            {
                Failure Errorresponse = new Failure();
                Errorresponse.ErrorMessage = ErrorsValueMapping.GetStringValue(ErrorDefinations.UserInvalidPassword);
                return Results.BadRequest(Errorresponse);
            }


            string token = _jwtTokenGenerator.GenerateTokenNormalUser(loginUser,
                        EnvValueGetter.Get<string>(EnvVarriableNamesMapping.GetStringValue(EnvVarriableNames.TokenSecretNormal)),
                        EnvValueGetter.Get<string>(EnvVarriableNamesMapping.GetStringValue(EnvVarriableNames.TokenIssuer)),
                        EnvValueGetter.Get<string>(EnvVarriableNamesMapping.GetStringValue(EnvVarriableNames.TokenAudience)),
                        EnvValueGetter.Get<int>(EnvVarriableNamesMapping.GetStringValue(EnvVarriableNames.TokenExpiryInMinutes)));

            
            Success sucessResponse = new Success{
                Email = loginUser.Email,
                Role = loginUser.Role,
                Token = token
            };

            return Results.Ok(sucessResponse);
        });


        app.MapPost("/user/register", async (Registrations registerrequest) => {

            if(await _userAuthentication.CheckUserByEmailAsync(registerrequest.Email))
            {
                Failure Errorresponse = new Failure();
                Errorresponse.ErrorMessage = ErrorsValueMapping.GetStringValue(ErrorDefinations.UserExists);
                return Results.BadRequest(Errorresponse);
            }

            NormalUser registeruser = new NormalUser {
                FirstName = registerrequest.FirstName,
                LaastName = registerrequest.LastName,
                Email = registerrequest.Email,
                ImageName = "N/A",
                ImagePath = "N/A",
                Password = BCrypt.Net.BCrypt.HashPassword(registerrequest.Password),
                Role = EnvValueGetter.Get<string>(EnvVarriableNamesMapping.GetStringValue(EnvVarriableNames.UserNormal)),
                IsActive = true
            };

            await _normalUserRepository.InsertAsync(registeruser);
            await _normalUserRepository.SaveChangesAsync();

            string token = _jwtTokenGenerator.GenerateTokenNormalUser(registeruser,
                        EnvValueGetter.Get<string>(EnvVarriableNamesMapping.GetStringValue(EnvVarriableNames.TokenSecretNormal)),
                        EnvValueGetter.Get<string>(EnvVarriableNamesMapping.GetStringValue(EnvVarriableNames.TokenIssuer)),
                        EnvValueGetter.Get<string>(EnvVarriableNamesMapping.GetStringValue(EnvVarriableNames.TokenAudience)),
                        EnvValueGetter.Get<int>(EnvVarriableNamesMapping.GetStringValue(EnvVarriableNames.TokenExpiryInMinutes)));

            Success sucessResponse = new Success{
                Email = registeruser.Email,
                Role = registeruser.Role,
                Token = token
            };

            return Results.Ok(sucessResponse);
        });

    }
}
