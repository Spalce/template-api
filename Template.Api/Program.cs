using Microsoft.EntityFrameworkCore;
using Template.Core.Interfaces.Repositories;
using Template.Core.Interfaces.Services;
using Template.Core.Services;
using Template.Infrastructure.Data;
using Template.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("Template.Api")));

builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();

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
