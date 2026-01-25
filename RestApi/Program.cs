
using Microsoft.EntityFrameworkCore;
using RestApi.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<RestApiDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ResetApiContextString") ?? throw new InvalidOperationException("Connection string 'ResetApiContextString' not found.")));

// Add services to the container
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
app.UseAuthorization();
app.MapControllers();

app.Run();
