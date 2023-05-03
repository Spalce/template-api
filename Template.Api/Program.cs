using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Template.Core.Interfaces.Repositories;
using Template.Core.Interfaces.Services;
using Template.Core.Services;
using Template.Infrastructure.Data;
using Template.Infrastructure.Models;
using Template.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("Template.Api")));

builder.Services.AddIdentity<AppUser, AppRole>(o =>
{
    o.SignIn.RequireConfirmedAccount = false;
    o.SignIn.RequireConfirmedEmail = false;
    o.SignIn.RequireConfirmedPhoneNumber = false;
    o.User.RequireUniqueEmail = true;
    o.Password.RequireDigit = true;
    o.Password.RequiredLength = 6;
    o.Password.RequireLowercase = true;
    o.Password.RequireUppercase = true;
    o.Password.RequireNonAlphanumeric = true;
    o.Password.RequireUppercase = false;
    o.Password.RequiredUniqueChars = 1;
    o.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    o.Lockout.MaxFailedAccessAttempts = 5;
    o.Lockout.AllowedForNewUsers = true;
}).AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

    builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
await using (var scope = app.Services.CreateAsyncScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await dataContext.Database.MigrateAsync();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
