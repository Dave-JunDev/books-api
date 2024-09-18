using Context;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Services;
using Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PostgresContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection"));
});


// Add services to the container.
builder.Services.AddScoped<IPaginationUtil, PaginationUtil>();
builder.Services.AddScoped<IBookService, BookService>();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
