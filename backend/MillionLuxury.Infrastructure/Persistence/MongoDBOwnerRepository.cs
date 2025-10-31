using MongoDB.Driver;
using MillionLuxury.Application.Interfaces;
using MillionLuxury.Domain.Entities;
using MillionLuxury.Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;

namespace MillionLuxury.Infrastructure.Persistence;

public class MongoDBOwnerRepository : IOwnerRepository
{
	private readonly IMongoCollection<Owner> _owners;

	public MongoDBOwnerRepository(IMongoDbSettings settings)
	{
		var client = new MongoClient(settings.ConnectionString);
		var database = client.GetDatabase(settings.DatabaseName);
		_owners = database.GetCollection<Owner>("Owners");
	}

	public async Task<IEnumerable<Owner>> GetAllAsync()
	{
		return await _owners.Find(_ => true).ToListAsync();
	}

	public async Task<Owner?> GetByIdAsync(ObjectId id)
	{
		var owner = await _owners.Find(p => p.Id == id).FirstOrDefaultAsync();
		if (owner == null)
		{
			throw new ApiException("Owner not found", StatusCodes.Status404NotFound);
		}
		return owner;
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

	public async Task<Owner> UpdateAsync(Owner owner)
	{
		if (owner.Id == ObjectId.Empty)
		{
			throw new ApiException("Invalid owner ID", StatusCodes.Status400BadRequest);
		}

		owner.UpdatedAt = DateTime.UtcNow;
		var result = await _owners.ReplaceOneAsync(p => p.Id == owner.Id, owner);

		if (result.MatchedCount == 0)
		{
			throw new ApiException("Owner not found", StatusCodes.Status404NotFound);
		}

		return owner;
	}

	public async Task DeleteAsync(ObjectId id)
	{
		var result = await _owners.DeleteOneAsync(p => p.Id == id);
		if (result.DeletedCount == 0)
		{
			throw new ApiException("Owner not found", StatusCodes.Status404NotFound);
		}
	}
}