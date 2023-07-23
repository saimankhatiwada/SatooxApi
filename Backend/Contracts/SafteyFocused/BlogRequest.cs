namespace Contracts.SafteyFocused;

public class BlogRequest
{
    public string Tittle { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Author { get; set;} = null!;
    public DateOnly Published { get; set; }
    public DateOnly Modified { get; set; }
    public Guid AdminId { get; set; }
}
