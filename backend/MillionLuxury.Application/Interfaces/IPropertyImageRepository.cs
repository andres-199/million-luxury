using MillionLuxury.Domain.Entities;
using MongoDB.Bson;

namespace MillionLuxury.Application.Interfaces;

public interface IPropertyImageRepository
{
	Task<PropertyImage> CreateAsync(PropertyImage image);
}