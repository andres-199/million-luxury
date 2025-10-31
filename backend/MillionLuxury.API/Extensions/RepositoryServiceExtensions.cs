using MillionLuxury.Application.Interfaces;
using MillionLuxury.Infrastructure.Persistence;

namespace MillionLuxury.API.Extensions;

public static class RepositoryServiceExtensions
{
	public static IServiceCollection AddRepositories(this IServiceCollection services)
	{
		services.AddScoped<IPropertyRepository, MongoDBPropertyRepository>();
		services.AddScoped<IOwnerRepository, MongoDBOwnerRepository>();
		services.AddScoped<IPropertyImageRepository, MongoDBPropertyImageRepository>();
		services.AddScoped<IPropertyTraceRepository, MongoDBPropertyTraceRepository>();

		return services;
	}
}
