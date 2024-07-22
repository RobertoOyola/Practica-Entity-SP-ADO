using Microsoft.EntityFrameworkCore;
using UsuarioAPI.Data;
using UsuarioAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSwaggerGen();

//injectar el controlador no olvidar
// Register repositories
builder.Services.AddScoped<UsuarioAdoRepository>(provider => new UsuarioAdoRepository(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<UsuarioEntityRepository>();
builder.Services.AddScoped<UsusarioSpRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
