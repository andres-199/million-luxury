using MillionLuxury.Domain.Entities;
using MongoDB.Bson;

namespace MillionLuxury.Application.Interfaces;

public interface IOwnerRepository
{
	Task<IEnumerable<Owner>> GetAllAsync();
	Task<Owner?> GetByIdAsync(ObjectId id);
	Task<Owner> CreateAsync(Owner owner);
	Task<Owner> UpdateAsync(Owner owner);
	Task DeleteAsync(ObjectId id);
}