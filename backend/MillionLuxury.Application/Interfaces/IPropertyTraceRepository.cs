using MillionLuxury.Domain.Entities;
using MongoDB.Bson;

namespace MillionLuxury.Application.Interfaces;

public interface IPropertyTraceRepository
{
	Task<IEnumerable<PropertyTrace>> GetByPropertyIdAsync(ObjectId propertyId);
	Task<PropertyTrace?> GetByIdAsync(ObjectId id);
	Task<PropertyTrace> CreateAsync(PropertyTrace trace);
}