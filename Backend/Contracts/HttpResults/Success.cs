namespace Contracts.HttpResults;

public class Success
{
    public int Code { get; set; }
    public string Email { get; set; } = null!;
    public string Role { get; set; } = null!;
    public string Token { get; set; } = null!;
}
