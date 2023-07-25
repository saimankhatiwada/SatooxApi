using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Data.Users.Admin;

namespace Data.Blogs;

public class Blog
{
    [Key]
    public Guid Id { get; set; }
    public string Tittle { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Author { get; set;} = null!;
    public string ImagePath { get; set; } = null!;
    public string ImageName { get; set; } = null!;
    public DateOnly Published { get; set; }
    public DateOnly Modified { get; set; }

    [ForeignKey(nameof(AdminUser))]
    public Guid AdminId { get; set; }
}
