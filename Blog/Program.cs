using Blog.Data;
using Blog.Repositories;
using Blog.Repositories.Interfaces;
using Blog.Services;
using Blog.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton<ConnectionDB>();
builder.Services.AddSingleton<CategoryRepository>();
builder.Services.AddSingleton<CategoryService>();

builder.Services.AddSingleton<RoleRepository>();
builder.Services.AddSingleton<RoleService>();

builder.Services.AddSingleton<TagRepository>();
builder.Services.AddSingleton<TagService>();

builder.Services.AddSingleton<UserRepository>();
builder.Services.AddSingleton<UserService>();



var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
