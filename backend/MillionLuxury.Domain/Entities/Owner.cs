using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MillionLuxury.Domain.Entities;

public class Owner
{
	[BsonId]
	[BsonRepresentation(BsonType.ObjectId)]
	public ObjectId Id { get; set; }

	public string Name { get; set; } = string.Empty;
	public string Address { get; set; } = string.Empty;
	public string Photo { get; set; } = string.Empty;
	public DateTime Birthday { get; set; }

	public ICollection<Property> Properties { get; set; } = new List<Property>();

	public DateTime CreatedAt { get; set; }
	public DateTime? UpdatedAt { get; set; }
}