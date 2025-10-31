using MongoDB.Bson;
using MongoDB.Driver;
using MillionLuxury.Application.Interfaces;
using MillionLuxury.Domain.Entities;
using MillionLuxury.Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;

namespace MillionLuxury.Infrastructure.Persistence;

public class MongoDBPropertyRepository : IPropertyRepository
{
	private readonly IMongoCollection<Property> _properties;

	public MongoDBPropertyRepository(IMongoDbSettings settings)
	{
		var client = new MongoClient(settings.ConnectionString);
		var database = client.GetDatabase(settings.DatabaseName);
		_properties = database.GetCollection<Property>("Properties");
	}

	public async Task<IEnumerable<Property>> GetAllAsync()
	{
		var properties = await _properties.Find(_ => true).ToListAsync();

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

	public async Task<Property> GetByIdAsync(ObjectId id)
	{
		var property = await _properties.Find(p => p.Id == id).FirstOrDefaultAsync();
		if (property == null)
		{
			throw new ApiException("Property not found", StatusCodes.Status404NotFound);
		}

		// Load related images and traces
		var propertyWithDetails = property;

		// Load images if they exist
		var images = await _properties.Database
			.GetCollection<PropertyImage>("PropertyImages")
			.Find(i => i.PropertyId == id)
			.ToListAsync();
		propertyWithDetails.Images = images;

		// Load traces if they exist
		var traces = await _properties.Database
			.GetCollection<PropertyTrace>("PropertyTraces")
			.Find(t => t.PropertyId == id)
			.ToListAsync();
		propertyWithDetails.Traces = traces;

		// Load owner if exists
		var owner = await _properties.Database
			.GetCollection<Owner>("Owners")
			.Find(o => o.Id == property.OwnerId)
			.FirstOrDefaultAsync();
		propertyWithDetails.Owner = owner;

		return propertyWithDetails;
	}

	public async Task<IEnumerable<Property>> GetByOwnerIdAsync(ObjectId ownerId)
	{
		return await _properties.Find(p => p.OwnerId == ownerId).ToListAsync();
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
			// Verify owner exists
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

	public async Task<Property> UpdateAsync(Property property)
	{
		if (property.Id == ObjectId.Empty)
		{
			throw new ApiException("Invalid property ID", StatusCodes.Status400BadRequest);
		}

		// Verify owner exists
		var ownerExists = await _properties.Database
			.GetCollection<Owner>("Owners")
			.Find(o => o.Id == property.OwnerId)
			.AnyAsync();

		if (!ownerExists)
		{
			throw new ApiException("Owner not found", StatusCodes.Status404NotFound);
		}

		property.UpdatedAt = DateTime.UtcNow;
		var result = await _properties.ReplaceOneAsync(p => p.Id == property.Id, property);

		if (result.MatchedCount == 0)
		{
			throw new ApiException("Property not found", StatusCodes.Status404NotFound);
		}

		return property;
	}

	public async Task DeleteAsync(ObjectId id)
	{
		// Delete associated images and traces first
		await _properties.Database
			.GetCollection<PropertyImage>("PropertyImages")
			.DeleteManyAsync(i => i.PropertyId == id);

		await _properties.Database
			.GetCollection<PropertyTrace>("PropertyTraces")
			.DeleteManyAsync(t => t.PropertyId == id);

		var result = await _properties.DeleteOneAsync(p => p.Id == id);
		if (result.DeletedCount == 0)
		{
			throw new ApiException("Property not found", StatusCodes.Status404NotFound);
		}
	}
}

