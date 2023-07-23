using Data.Users.Admin;
using Data.Users.Normal;

namespace BussinessLogic.Logic.Security.JWT.ITokenGenerator;

public interface IJWTTokenGenerator
{
    public string GenerateTokenNormalUser(NormalUser user, string securitykey, string issure, string audience, int time);

    public string GenerateTokenSystemUser(AdminUser admin, string securitykey, string issure, string audience, int time);
}
