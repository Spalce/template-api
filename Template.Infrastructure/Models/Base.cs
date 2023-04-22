using System.ComponentModel.DataAnnotations;

namespace Template.Infrastructure.Models;

public class Base
{
    [Key]
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public DateTime? DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
}
