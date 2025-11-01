using MongoDB.Bson;
using MongoDB.Driver;
using MillionLuxury.Application.Interfaces;
using MillionLuxury.Domain.Entities;
using MillionLuxury.Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MillionLuxury.Infrastructure.Common.Constants;

namespace MillionLuxury.Infrastructure.Persistence;

public class MongoDBPropertyRepository : IPropertyRepository
{
	private readonly IMongoCollection<Property> _properties;
	private readonly ILogger<MongoDBPropertyRepository> _logger;

	public MongoDBPropertyRepository(IMongoDbSettings settings, ILogger<MongoDBPropertyRepository> logger)
	{
		_logger = logger;
		var client = new MongoClient(settings.ConnectionString);
		var database = client.GetDatabase(settings.DatabaseName);
		_properties = database.GetCollection<Property>(MongoCollections.Properties);


		try
		{
			var textIndex = new CreateIndexModel<Property>(Builders<Property>.IndexKeys.Text(p => p.Name).Text(p => p.Address));
			var priceIndex = new CreateIndexModel<Property>(Builders<Property>.IndexKeys.Ascending(p => p.Price));
			_properties.Indexes.CreateMany(new[] { textIndex, priceIndex });
			_logger.LogInformation("MongoDB indexes created successfully for Properties collection");
		}
		catch (Exception ex)
		{
			_logger.LogWarning(ex, "Failed to create MongoDB indexes for Properties collection. Indexes may already exist or there may be a configuration issue.");
		}
	}

	public async Task<Property> GetByIdAsync(ObjectId id)
	{
		var property = await GetPropertyWithRelations(id);
		if (property == null)
		{
			throw new ApiException("Property not found", StatusCodes.Status404NotFound);
		}

		return property;
	}

	public async Task<IEnumerable<Property>> GetByFilterAsync(string? name, string? address, decimal? minPrice, decimal? maxPrice, int page, int pageSize)
	{
		var filterBuilder = Builders<Property>.Filter;
		var filters = new List<FilterDefinition<Property>>();
		if (!string.IsNullOrWhiteSpace(name) || !string.IsNullOrWhiteSpace(address))
		{
			var searchConditions = new List<FilterDefinition<Property>>();

			if (!string.IsNullOrWhiteSpace(name))
			{
				searchConditions.Add(filterBuilder.Regex(p => p.Name, new BsonRegularExpression(name, "i")));
			}

			if (!string.IsNullOrWhiteSpace(address))
			{
				searchConditions.Add(filterBuilder.Regex(p => p.Address, new BsonRegularExpression(address, "i")));
			}

			filters.Add(filterBuilder.Or(searchConditions));
		}

		if (minPrice.HasValue)
		{
			filters.Add(filterBuilder.Gte(p => p.Price, minPrice.Value));
		}

		if (maxPrice.HasValue)
		{
			filters.Add(filterBuilder.Lte(p => p.Price, maxPrice.Value));
		}

		var finalFilter = filters.Count > 0 ? filterBuilder.And(filters) : filterBuilder.Empty;

		var properties = await _properties.Find(finalFilter)
			.Skip((page - 1) * pageSize)
			.Limit(pageSize)
			.ToListAsync();


		var ownerIds = properties.Select(p => p.OwnerId).Distinct().ToList();
		var propertyIds = properties.Select(p => p.Id).ToList();

		var owners = await _properties.Database
			.GetCollection<Owner>(MongoCollections.Owners)
			.Find(o => ownerIds.Contains(o.Id))
			.ToListAsync();

		var images = await _properties.Database
			.GetCollection<PropertyImage>(MongoCollections.PropertyImages)
			.Find(i => propertyIds.Contains(i.PropertyId))
			.ToListAsync();

		var traces = await _properties.Database
			.GetCollection<PropertyTrace>(MongoCollections.PropertyTraces)
			.Find(t => propertyIds.Contains(t.PropertyId))
			.ToListAsync();


		foreach (var property in properties)
		{
			property.Owner = owners.FirstOrDefault(o => o.Id == property.OwnerId);
			property.Images = images.Where(i => i.PropertyId == property.Id).ToList();
			property.Traces = traces.Where(t => t.PropertyId == property.Id).ToList();
		}

		return properties;
	}

	public async Task<Property> CreateAsync(Property property)
	{
		property.CreatedAt = DateTime.UtcNow;
		if (property.Id == ObjectId.Empty)
		{
			property.Id = ObjectId.GenerateNewId();
		}

		try
		{
			var ownerExists = await _properties.Database
				.GetCollection<Owner>(MongoCollections.Owners)
				.Find(o => o.Id == property.OwnerId)
				.AnyAsync();

			if (!ownerExists)
			{
				throw new ApiException("Owner not found", StatusCodes.Status404NotFound);
			}

			await _properties.InsertOneAsync(property);
			return await GetPropertyWithRelations(property.Id);
		}
		catch (MongoWriteException ex) when (ex.WriteError.Category == ServerErrorCategory.DuplicateKey)
		{
			throw new ApiException("A property with this ID already exists", StatusCodes.Status409Conflict);
		}
	}

	private async Task<Property?> GetPropertyWithRelations(ObjectId id)
	{
		var property = await _properties.Find(p => p.Id == id).FirstOrDefaultAsync();

		if (property == null)
		{
			return null;
		}

		property.Owner = await _properties.Database
			.GetCollection<Owner>(MongoCollections.Owners)
			.Find(o => o.Id == property.OwnerId)
			.FirstOrDefaultAsync();

		property.Images = await _properties.Database
			.GetCollection<PropertyImage>(MongoCollections.PropertyImages)
			.Find(i => i.PropertyId == property.Id)
			.ToListAsync();

		property.Traces = await _properties.Database
			.GetCollection<PropertyTrace>(MongoCollections.PropertyTraces)
			.Find(t => t.PropertyId == property.Id)
			.ToListAsync();

		return property;
	}
}

