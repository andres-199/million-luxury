namespace MillionLuxury.Application.Interfaces;

public interface IMongoDbSettings
{
	string ConnectionString { get; }
	string DatabaseName { get; }
}