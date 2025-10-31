using MediatR;
using MongoDB.Bson;
using MillionLuxury.Application.Interfaces;
using MillionLuxury.Domain.Entities;
using MillionLuxury.Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;

namespace MillionLuxury.Application.Features.PropertyImages.Commands;

public record CreatePropertyImageCommand : IRequest<PropertyImage>
{
	public required string File { get; init; }
	public bool Enabled { get; init; } = true;

	public string PropertyId { get; init; } = string.Empty;
}

public class CreatePropertyImageCommandHandler : IRequestHandler<CreatePropertyImageCommand, PropertyImage>
{
	private readonly IPropertyImageRepository _imageRepository;
	private readonly IPropertyRepository _propertyRepository;

	public CreatePropertyImageCommandHandler(
			IPropertyImageRepository imageRepository,
			IPropertyRepository propertyRepository)
	{
		_imageRepository = imageRepository;
		_propertyRepository = propertyRepository;
	}

	public async Task<PropertyImage> Handle(CreatePropertyImageCommand request, CancellationToken cancellationToken)
	{
		if (!ObjectId.TryParse(request.PropertyId, out var propertyId))
		{
			throw new ApiException("Invalid property ID format", StatusCodes.Status400BadRequest);
		}

		await _propertyRepository.GetByIdAsync(propertyId);

		var propertyImage = new PropertyImage
		{
			PropertyId = propertyId,
			File = request.File,
			Enabled = request.Enabled
		};

		return await _imageRepository.CreateAsync(propertyImage);
	}
}