using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MillionLuxury.Domain.Entities;

public class PropertyImage
{
	[BsonId]
	[BsonRepresentation(BsonType.ObjectId)]
	public ObjectId Id { get; set; }

	[BsonRepresentation(BsonType.ObjectId)]
	public required ObjectId PropertyId { get; set; }

	public required string File { get; set; }
	public bool Enabled { get; set; } = true;

	public DateTime CreatedAt { get; set; }
	public DateTime? UpdatedAt { get; set; }
}