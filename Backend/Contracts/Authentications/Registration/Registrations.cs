namespace Contracts.Authentications.Registration;

public class Registrations
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string ImagePath { get; set; } = null!;
    public string ImageName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public bool IsActive { get; set; }
}
