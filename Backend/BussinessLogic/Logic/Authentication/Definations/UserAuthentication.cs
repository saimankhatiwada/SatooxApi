using BussinessLogic.Logic.Authentication.IDefinations;
using Data.DatabaseContext;
using Data.Users.Normal;
using Microsoft.EntityFrameworkCore;

namespace BussinessLogic.Logic.Authentication.Definations;

public class UserAuthentication : IUserAuthentication
{
    public readonly ApplicationDbContext _context;

    public UserAuthentication(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<bool> CheckUserByEmailAsync(string email)
    {
        NormalUser? user = await _context.NormalUsers.FirstOrDefaultAsync(options => options.Email == email);
        if(String.IsNullOrEmpty(user?.Email))
        {
            return false;
        }
        return true;
    }

    public async Task<NormalUser> GetUserByEmailAsync(string email)
    {
        NormalUser? user = await _context.NormalUsers.FirstOrDefaultAsync(options => options.Email == email);
        if(user == null)
        {
            return new NormalUser();
        }
        return user;
    }
}
