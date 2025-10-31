using MediatR;
using MongoDB.Bson;
using MillionLuxury.Application.Interfaces;
using MillionLuxury.Domain.Entities;
using MillionLuxury.Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;

namespace MillionLuxury.Application.Features.Properties.Commands;

public record CreatePropertyCommand : IRequest<Property>
{
	public required string Name { get; init; }
	public required string Address { get; init; }
	public decimal Price { get; init; }
	public required string CodeInternal { get; init; }
	public int Year { get; init; }
	public required string OwnerId { get; init; }
}

public class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommand, Property>
{
	private readonly IPropertyRepository _propertyRepository;

	public CreatePropertyCommandHandler(IPropertyRepository propertyRepository)
	{
		_propertyRepository = propertyRepository;
	}

	public async Task<Property> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
	{
		if (!ObjectId.TryParse(request.OwnerId, out var ownerId))
		{
			throw new ApiException("Invalid owner ID format", StatusCodes.Status400BadRequest);
		}

		var property = new Property
		{
			Name = request.Name,
			Address = request.Address,
			Price = request.Price,
			CodeInternal = request.CodeInternal,
			Year = request.Year,
			OwnerId = ownerId,
			Images = new List<PropertyImage>(),
			Traces = new List<PropertyTrace>()
		};

		return await _propertyRepository.CreateAsync(property);
	}
}