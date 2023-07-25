namespace Contracts.HttpResults;

public class Failure
{
    public int Code { get; set; }
    public string ErrorMessage { get; set; } = null!;
}
