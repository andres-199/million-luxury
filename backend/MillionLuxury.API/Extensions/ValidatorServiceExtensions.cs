using FluentValidation;
using MillionLuxury.Application.Features.Owners.Commands;
using MillionLuxury.Application.Features.Properties.Commands;
using MillionLuxury.Application.Features.PropertyImages.Commands;
using MillionLuxury.Application.Features.PropertyTraces.Commands;

namespace MillionLuxury.API.Extensions;

public static class ValidatorServiceExtensions
{
	public static IServiceCollection AddValidators(this IServiceCollection services)
	{
		services.AddScoped<IValidator<CreatePropertyCommand>, CreatePropertyCommandValidator>();
		services.AddScoped<IValidator<CreateOwnerCommand>, CreateOwnerCommandValidator>();
		services.AddScoped<IValidator<CreatePropertyImageCommand>, CreatePropertyImageCommandValidator>();
		services.AddScoped<IValidator<CreatePropertyTraceCommand>, CreatePropertyTraceCommandValidator>();

		return services;
	}
}
