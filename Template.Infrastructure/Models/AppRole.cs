using Microsoft.AspNetCore.Identity;

namespace Template.Infrastructure.Models;

public class AppRole : IdentityRole
{
    public string? Description { get; set; }
}
