using MillionLuxury.Domain.Entities;
using MongoDB.Bson;

namespace MillionLuxury.Application.Interfaces;

public interface IPropertyImageRepository
{
	Task<IEnumerable<PropertyImage>> GetByPropertyIdAsync(ObjectId propertyId);
	Task<PropertyImage?> GetByIdAsync(ObjectId id);
	Task<PropertyImage> CreateAsync(PropertyImage image);
	Task<PropertyImage> UpdateAsync(PropertyImage image);
	Task DeleteAsync(ObjectId id);
}