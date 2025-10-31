using MillionLuxury.Application.Interfaces;
using MillionLuxury.Infrastructure.Settings;

namespace MillionLuxury.API.Extensions;

public static class DatabaseServiceExtensions
{
	public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services)
	{
		services.AddSingleton<IMongoDbSettings, MongoDbSettings>();

		return services;
	}
}
