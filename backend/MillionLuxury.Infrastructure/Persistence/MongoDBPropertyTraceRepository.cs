using MongoDB.Driver;
using MillionLuxury.Application.Interfaces;
using MillionLuxury.Domain.Entities;
using MillionLuxury.Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MillionLuxury.Infrastructure.Common.Constants;

namespace MillionLuxury.Infrastructure.Persistence;

public class MongoDBPropertyTraceRepository : IPropertyTraceRepository
{
	private readonly IMongoCollection<PropertyTrace> _traces;

	public MongoDBPropertyTraceRepository(IMongoDbSettings settings)
	{
		var client = new MongoClient(settings.ConnectionString);
		var database = client.GetDatabase(settings.DatabaseName);
		_traces = database.GetCollection<PropertyTrace>(MongoCollections.PropertyTraces);
	}

	public async Task<PropertyTrace> CreateAsync(PropertyTrace trace)
	{
		trace.CreatedAt = DateTime.UtcNow;
		if (trace.Id == ObjectId.Empty)
		{
			trace.Id = ObjectId.GenerateNewId();
		}

		try
		{
			await _traces.InsertOneAsync(trace);
			return trace;
		}
		catch (MongoWriteException ex) when (ex.WriteError.Category == ServerErrorCategory.DuplicateKey)
		{
			throw new ApiException("A property trace with this ID already exists", StatusCodes.Status409Conflict);
		}
	}
}