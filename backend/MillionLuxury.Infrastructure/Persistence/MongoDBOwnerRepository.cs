using MongoDB.Driver;
using MillionLuxury.Application.Interfaces;
using MillionLuxury.Domain.Entities;
using MillionLuxury.Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MillionLuxury.Infrastructure.Common.Constants;

namespace MillionLuxury.Infrastructure.Persistence;

public class MongoDBOwnerRepository : IOwnerRepository
{
	private readonly IMongoCollection<Owner> _owners;

	public MongoDBOwnerRepository(IMongoDbSettings settings)
	{
		var client = new MongoClient(settings.ConnectionString);
		var database = client.GetDatabase(settings.DatabaseName);
		_owners = database.GetCollection<Owner>(MongoCollections.Owners);
	}

	public async Task<Owner> CreateAsync(Owner owner)
	{
		owner.CreatedAt = DateTime.UtcNow;
		if (owner.Id == ObjectId.Empty)
		{
			owner.Id = ObjectId.GenerateNewId();
		}

		try
		{
			await _owners.InsertOneAsync(owner);
			return owner;
		}
		catch (MongoWriteException ex) when (ex.WriteError.Category == ServerErrorCategory.DuplicateKey)
		{
			throw new ApiException("An owner with this ID already exists", StatusCodes.Status409Conflict);
		}
	}
}