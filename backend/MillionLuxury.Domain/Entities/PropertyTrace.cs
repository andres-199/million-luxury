using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MillionLuxury.Domain.Entities;

public class PropertyTrace
{
	[BsonId]
	[BsonRepresentation(BsonType.ObjectId)]
	public ObjectId Id { get; set; }

	public DateTime DateSale { get; set; }
	public required string Name { get; set; }
	public decimal Value { get; set; }
	public decimal Tax { get; set; }

	[BsonRepresentation(BsonType.ObjectId)]
	public required ObjectId PropertyId { get; set; }

	public DateTime CreatedAt { get; set; }
	public DateTime? UpdatedAt { get; set; }
}