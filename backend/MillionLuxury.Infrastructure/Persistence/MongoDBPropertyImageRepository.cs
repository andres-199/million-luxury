using MongoDB.Driver;
using MillionLuxury.Application.Interfaces;
using MillionLuxury.Domain.Entities;
using MillionLuxury.Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;

namespace MillionLuxury.Infrastructure.Persistence;

public class MongoDBPropertyImageRepository : IPropertyImageRepository
{
	private readonly IMongoCollection<PropertyImage> _images;

	public MongoDBPropertyImageRepository(IMongoDbSettings settings)
	{
		var client = new MongoClient(settings.ConnectionString);
		var database = client.GetDatabase(settings.DatabaseName);
		_images = database.GetCollection<PropertyImage>("PropertyImages");
	}

	public async Task<IEnumerable<PropertyImage>> GetByPropertyIdAsync(ObjectId propertyId)
	{
		return await _images.Find(x => x.PropertyId == propertyId).ToListAsync();
	}

	public async Task<PropertyImage?> GetByIdAsync(ObjectId id)
	{
		var image = await _images.Find(p => p.Id == id).FirstOrDefaultAsync();
		if (image == null)
		{
			throw new ApiException("Property image not found", StatusCodes.Status404NotFound);
		}
		return image;
	}

	public async Task<PropertyImage> CreateAsync(PropertyImage image)
	{
		image.CreatedAt = DateTime.UtcNow;
		if (image.Id == ObjectId.Empty)
		{
			image.Id = ObjectId.GenerateNewId();
		}

		try
		{
			await _images.InsertOneAsync(image);
			return image;
		}
		catch (MongoWriteException ex) when (ex.WriteError.Category == ServerErrorCategory.DuplicateKey)
		{
			throw new ApiException("A property image with this ID already exists", StatusCodes.Status409Conflict);
		}
	}

	public async Task<PropertyImage> UpdateAsync(PropertyImage image)
	{
		if (image.Id == ObjectId.Empty)
		{
			throw new ApiException("Invalid property image ID", StatusCodes.Status400BadRequest);
		}

		image.UpdatedAt = DateTime.UtcNow;
		var result = await _images.ReplaceOneAsync(p => p.Id == image.Id, image);

		if (result.MatchedCount == 0)
		{
			throw new ApiException("Property image not found", StatusCodes.Status404NotFound);
		}

		return image;
	}

	public async Task DeleteAsync(ObjectId id)
	{
		var result = await _images.DeleteOneAsync(p => p.Id == id);
		if (result.DeletedCount == 0)
		{
			throw new ApiException("Property image not found", StatusCodes.Status404NotFound);
		}
	}
}