using Data.Users.Normal;

namespace BussinessLogic.Logic.Authentication.IDefinations;

public interface IUserAuthentication
{
    Task<bool> CheckUserByEmailAsync(string email);
    Task<NormalUser> GetUserByEmailAsync(string email);
}
