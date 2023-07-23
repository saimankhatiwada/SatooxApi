using BussinessLogic.Logic.Authentication.IDefinations;
using Data.DatabaseContext;
using Data.Users.Admin;
using Microsoft.EntityFrameworkCore;

namespace BussinessLogic.Logic.Authentication.Definations;

public class AdminAuthentication : IAdminAuthentication
{
    public readonly ApplicationDbContext _context;

    public AdminAuthentication(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<bool> CheckAdminByEmailAsync(string email)
    {
        AdminUser? admin = await _context.AdminUsers.FirstOrDefaultAsync(options => options.Email == email);
        if(String.IsNullOrEmpty(admin?.Email))
        {
            return false;
        }
        return true;
    }

    public async Task<AdminUser> GetAdminByEmailAsync(string email)
    {
        AdminUser? admin = await _context.AdminUsers.FirstOrDefaultAsync(options => options.Email == email);
        if(admin == null)
        {
            return new AdminUser();
        }
        return admin;
    }
}
