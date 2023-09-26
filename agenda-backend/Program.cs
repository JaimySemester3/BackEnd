using agenda_backend;
using MySql.EntityFrameworkCore.Extensions;
using MySql.Data.MySqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

builder.Services.AddDbContext<PlannerDbContext>(options =>
                options.UseMySQL("server=localhost;port=3303;database=plannerdatabase;uid=root;pwd=admin"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
                .WithOrigins("http://localhost:3000")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                );

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
