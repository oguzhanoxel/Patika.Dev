using System.Reflection;
using Microsoft.EntityFrameworkCore;
using WebApi.DbAccess;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddDbContext<BookStoreDbContext>(options => options.UseInMemoryDatabase(databaseName: "BookStoreDb"));
builder.Services.AddScoped<IBookStoreDbContext>(provider => provider.GetService<BookStoreDbContext>());
builder.Services.AddSingleton<ILoggerService, ConsoleLogger>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    BookStoreDataGenerator.Initialize(app.Services.CreateScope().ServiceProvider);
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCustomExceptionMiddleware();

app.MapControllers();

app.Run();
