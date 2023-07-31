namespace Contracts.HttpResults;

public class Success
{
    public int Code { get; set; }
    public string Token { get; set; } = null!;
}
