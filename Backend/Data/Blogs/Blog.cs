using System.ComponentModel.DataAnnotations;

namespace Data.Blogs;

public class Blog
{
    [Key]
    public Guid Id { get; set; }

    public string Tittle { get; set; } = null!;
}
