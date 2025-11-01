using MillionLuxury.API.Middleware;
using MillionLuxury.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCorsConfiguration();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatRConfiguration();

builder.Services.AddValidators();

builder.Services.AddDatabaseConfiguration();

builder.Services.AddRepositories();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseCors("AllowLocalhost");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
