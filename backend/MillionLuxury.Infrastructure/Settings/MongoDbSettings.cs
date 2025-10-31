using MillionLuxury.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace MillionLuxury.Infrastructure.Settings;

public class MongoDbSettings : IMongoDbSettings
{
	private readonly IConfiguration _configuration;

	public MongoDbSettings(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	public string ConnectionString => _configuration.GetConnectionString("MongoDB") ?? "mongodb://localhost:27017";
	public string DatabaseName => _configuration["MongoDB:DatabaseName"] ?? "MillionLuxuryDB";
}