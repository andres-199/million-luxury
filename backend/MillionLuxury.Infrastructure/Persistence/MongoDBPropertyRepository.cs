using MongoDB.Bson;
using MongoDB.Driver;
using MillionLuxury.Application.Interfaces;
using MillionLuxury.Domain.Entities;
using MillionLuxury.Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

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
		_properties = database.GetCollection<Property>("Properties");


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
		var property = await _properties.Find(p => p.Id == id).FirstOrDefaultAsync();
		if (property == null)
		{
			throw new ApiException("Property not found", StatusCodes.Status404NotFound);
		}

		var propertyWithDetails = property;

		var images = await _properties.Database
			.GetCollection<PropertyImage>("PropertyImages")
			.Find(i => i.PropertyId == id)
			.ToListAsync();
		propertyWithDetails.Images = images;

		var traces = await _properties.Database
			.GetCollection<PropertyTrace>("PropertyTraces")
			.Find(t => t.PropertyId == id)
			.ToListAsync();
		propertyWithDetails.Traces = traces;

		var owner = await _properties.Database
			.GetCollection<Owner>("Owners")
			.Find(o => o.Id == property.OwnerId)
			.FirstOrDefaultAsync();
		propertyWithDetails.Owner = owner;

		return propertyWithDetails;
	}

	public async Task<IEnumerable<Property>> GetByFilterAsync(string? name, string? address, decimal? minPrice, decimal? maxPrice)
	{
		var filterBuilder = Builders<Property>.Filter;
		var filters = new List<FilterDefinition<Property>>();

		if (!string.IsNullOrWhiteSpace(name))
		{
			filters.Add(filterBuilder.Text(name));
		}

		if (!string.IsNullOrWhiteSpace(address))
		{
			filters.Add(filterBuilder.Regex(p => p.Address, new BsonRegularExpression(address, "i")));
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

		var properties = await _properties.Find(finalFilter).ToListAsync();

		foreach (var property in properties)
		{
			property.Owner = await _properties.Database
				.GetCollection<Owner>("Owners")
				.Find(o => o.Id == property.OwnerId)
				.FirstOrDefaultAsync();


			property.Images = await _properties.Database
				.GetCollection<PropertyImage>("PropertyImages")
				.Find(i => i.PropertyId == property.Id)
				.ToListAsync();


			property.Traces = await _properties.Database
				.GetCollection<PropertyTrace>("PropertyTraces")
				.Find(t => t.PropertyId == property.Id)
				.ToListAsync();
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
				.GetCollection<Owner>("Owners")
				.Find(o => o.Id == property.OwnerId)
				.AnyAsync();

			if (!ownerExists)
			{
				throw new ApiException("Owner not found", StatusCodes.Status404NotFound);
			}

			await _properties.InsertOneAsync(property);
			return property;
		}
		catch (MongoWriteException ex) when (ex.WriteError.Category == ServerErrorCategory.DuplicateKey)
		{
			throw new ApiException("A property with this ID already exists", StatusCodes.Status409Conflict);
		}
	}
}

