using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Template.Infrastructure.Models;

namespace Template.Infrastructure.Data;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {}

    public virtual DbSet<Student> Students { get; set; } = null!;
    public virtual DbSet<Contact> Contacts { get; set; } = null!;
}
