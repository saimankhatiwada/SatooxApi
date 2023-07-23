using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BussinessLogic.Logic.Security.JWT.ITokenGenerator;
using BussinessLogic.Logic.Security.Service.IService;
using Data.Users.Admin;
using Data.Users.Normal;
using Microsoft.IdentityModel.Tokens;

namespace BussinessLogic.Logic.Security.JWT.TokenGenerator;

public class JWTTokenGenerator : IJWTTokenGenerator
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public JWTTokenGenerator(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }
    public string GenerateTokenNormalUser(NormalUser user, string securitykey, string issure, string audience, int time)
    {
        SigningCredentials siginingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(securitykey)),
                SecurityAlgorithms.HmacSha256
        );

        Claim[] claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.LaastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        JwtSecurityToken securityToken = new JwtSecurityToken(
            issuer: issure,
            audience: audience,
            expires: _dateTimeProvider.Now.AddMinutes(time).DateTime,
            claims: claims,
            signingCredentials: siginingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }

    public string GenerateTokenSystemUser(AdminUser admin, string securitykey, string issure, string audience, int time)
    {
        SigningCredentials siginingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(securitykey)),
                SecurityAlgorithms.HmacSha256
        );

        Claim[] claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, admin.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, admin.FirstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, admin.LaastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        JwtSecurityToken securityToken = new JwtSecurityToken(
            issuer: issure,
            audience: audience,
            expires: _dateTimeProvider.Now.AddMinutes(time).DateTime,
            claims: claims,
            signingCredentials: siginingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}
