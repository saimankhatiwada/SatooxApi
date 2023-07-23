using System.ComponentModel.DataAnnotations;

namespace Data.Users.Normal;

public class NormalUser
{
    [Key]
    public Guid Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LaastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string ImagePath { get; set; } = null!;

    public string ImageName { get; set; } = null!;

    public string Role { get; set; } = null!;

    public bool IsActive { get; set; }
}
