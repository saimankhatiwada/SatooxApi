using Microsoft.EntityFrameworkCore;
using Data.Users.Normal;
using Data.Users.Admin;
using Data.Blogs;

namespace Data.DatabaseContext;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<AdminUser>().HasIndex(u => u.Email).IsUnique();
        builder.Entity<AdminUser>().HasIndex(u => u.Id).IsUnique();
        builder.Entity<NormalUser>().HasIndex(u => u.Email).IsUnique();
        builder.Entity<NormalUser>().HasIndex(u => u.Id).IsUnique();
        builder.Entity<Blog>().HasIndex(u => u.Id).IsUnique();
    }
    public DbSet<AdminUser> AdminUsers { get; set; } = null!;
    public DbSet<NormalUser> NormalUsers { get; set; } = null!;
    public DbSet<Blog> Blogs { get; set; } = null!;
}
