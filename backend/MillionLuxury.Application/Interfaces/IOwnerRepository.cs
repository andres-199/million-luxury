using MillionLuxury.Domain.Entities;
using MongoDB.Bson;

namespace MillionLuxury.Application.Interfaces;

public interface IOwnerRepository
{
	Task<Owner> CreateAsync(Owner owner);
}