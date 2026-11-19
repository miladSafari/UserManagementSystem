using Microsoft.EntityFrameworkCore;
using UserManagementService.gRPCService.Services;
using UserManagementSystem.Domain.Interfaces.Repositories.Users;
using UserManagementSystem.Domain.Interfaces.Services.Users;
using UserManagementSystem.Infrastructure.Data;
using UserManagementSystem.Infrastructure.Repositories.Users;
using UserManagementSystem.Service.Services.Users;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UserManagementContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("UserManagementConnection"));
});


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddGrpc();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();                       

app.MapGrpcService<UserGrpcService>();     

app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();