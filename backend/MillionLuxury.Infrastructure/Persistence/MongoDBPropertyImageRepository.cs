using MongoDB.Driver;
using MillionLuxury.Application.Interfaces;
using MillionLuxury.Domain.Entities;
using MillionLuxury.Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MillionLuxury.Infrastructure.Common.Constants;

namespace MillionLuxury.Infrastructure.Persistence;

public class MongoDBPropertyImageRepository : IPropertyImageRepository
{
	private readonly IMongoCollection<PropertyImage> _images;

	public MongoDBPropertyImageRepository(IMongoDbSettings settings)
	{
		var client = new MongoClient(settings.ConnectionString);
		var database = client.GetDatabase(settings.DatabaseName);
		_images = database.GetCollection<PropertyImage>(MongoCollections.PropertyImages);
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
}