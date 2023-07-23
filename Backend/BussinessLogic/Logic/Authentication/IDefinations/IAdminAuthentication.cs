using Data.Users.Admin;

namespace BussinessLogic.Logic.Authentication.IDefinations;

public interface IAdminAuthentication
{
    Task<bool> CheckAdminByEmailAsync(string email);
    Task<AdminUser> GetAdminByEmailAsync(string email);
}
