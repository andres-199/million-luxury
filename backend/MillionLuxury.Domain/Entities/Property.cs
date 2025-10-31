using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MillionLuxury.Domain.Entities;

public class Property
{
	[BsonId]
	[BsonRepresentation(BsonType.ObjectId)]
	public ObjectId Id { get; set; }

	public required string Name { get; set; }
	public required string Address { get; set; }
	public decimal Price { get; set; }
	public required string CodeInternal { get; set; }
	public int Year { get; set; }

	[BsonRepresentation(BsonType.ObjectId)]
	public required ObjectId OwnerId { get; set; }

	[BsonIgnore]
	public Owner? Owner { get; set; }

	public ICollection<PropertyImage> Images { get; set; } = new List<PropertyImage>();
	public ICollection<PropertyTrace> Traces { get; set; } = new List<PropertyTrace>();

	public DateTime CreatedAt { get; set; }
	public DateTime? UpdatedAt { get; set; }
}