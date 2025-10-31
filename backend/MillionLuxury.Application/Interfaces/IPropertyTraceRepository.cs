using MillionLuxury.Domain.Entities;
using MongoDB.Bson;

namespace MillionLuxury.Application.Interfaces;

public interface IPropertyTraceRepository
{
	Task<PropertyTrace> CreateAsync(PropertyTrace trace);
}