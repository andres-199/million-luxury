using System.Reflection;
using MediatR;
using MillionLuxury.Application.Common.Behaviors;

namespace MillionLuxury.API.Extensions;

public static class MediatRServiceExtensions
{
	public static IServiceCollection AddMediatRConfiguration(this IServiceCollection services)
	{
		services.AddMediatR(cfg =>
		{
			cfg.RegisterServicesFromAssembly(Assembly.Load("MillionLuxury.Application"));
			cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
		});

		return services;
	}
}
