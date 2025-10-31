using MongoDB.Bson;
using MillionLuxury.Domain.Entities;

namespace MillionLuxury.Application.Interfaces;

public interface IPropertyRepository
{
	Task<IEnumerable<Property>> GetByFilterAsync(string? name, string? address, decimal? minPrice, decimal? maxPrice, int page, int pageSize);
	Task<Property> GetByIdAsync(ObjectId id);
	Task<Property> CreateAsync(Property property);
}