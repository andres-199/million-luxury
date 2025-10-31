using System.Reflection;
using FluentValidation;
using MediatR;
using MillionLuxury.Application.Common.Behaviors;
using MillionLuxury.Application.Features.Properties.Commands;
using MillionLuxury.Application.Features.Owners.Commands;
using MillionLuxury.Application.Features.PropertyImages.Commands;
using MillionLuxury.Application.Features.PropertyTraces.Commands;
using MillionLuxury.Application.Interfaces;
using MillionLuxury.Infrastructure.Persistence;
using MillionLuxury.Infrastructure.Settings;
using MillionLuxury.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.Load("MillionLuxury.Application"));
    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
});


builder.Services.AddScoped<IValidator<CreatePropertyCommand>, CreatePropertyCommandValidator>();
builder.Services.AddScoped<IValidator<CreateOwnerCommand>, CreateOwnerCommandValidator>();
builder.Services.AddScoped<IValidator<CreatePropertyImageCommand>, CreatePropertyImageCommandValidator>();
builder.Services.AddScoped<IValidator<CreatePropertyTraceCommand>, CreatePropertyTraceCommandValidator>();

builder.Services.AddSingleton<IMongoDbSettings, MongoDbSettings>();

builder.Services.AddScoped<IPropertyRepository, MongoDBPropertyRepository>();
builder.Services.AddScoped<IOwnerRepository, MongoDBOwnerRepository>();
builder.Services.AddScoped<IPropertyImageRepository, MongoDBPropertyImageRepository>();
builder.Services.AddScoped<IPropertyTraceRepository, MongoDBPropertyTraceRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
