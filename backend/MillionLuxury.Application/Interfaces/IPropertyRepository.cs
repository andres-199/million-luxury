using MongoDB.Bson;
using MillionLuxury.Domain.Entities;

namespace MillionLuxury.Application.Interfaces;

public interface IPropertyRepository
{
	Task<IEnumerable<Property>> GetAllAsync();
	Task<Property> GetByIdAsync(ObjectId id);
	Task<IEnumerable<Property>> GetByOwnerIdAsync(ObjectId ownerId);
	Task<Property> CreateAsync(Property property);
	Task<Property> UpdateAsync(Property property);
	Task DeleteAsync(ObjectId id);
}