using Microsoft.EntityFrameworkCore;
using TodoListWebAPIs.DataAccessLayers;
using TodoListWebAPIs.DTOs.Requests;
using TodoListWebAPIs.DTOs.Responses;
using TodoListWebAPIs.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddControllers(options =>
//{
//    options.RespectBrowserAcceptHeader = true;
//}).AddXmlSerializerFormatters();

builder.Services.AddDbContext<TodoDBContext>(options => options.UseInMemoryDatabase("Todos"));
builder.Services.AddScoped<IRepository<TodoRequest, TodoResponse, int>, TodoRepository>();

builder.Services.AddCors();

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

app.UseCors(
        options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
    );

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
