using MediatR;
using MongoDB.Bson;
using MillionLuxury.Application.Interfaces;
using MillionLuxury.Domain.Entities;
using MillionLuxury.Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;

namespace MillionLuxury.Application.Features.PropertyTraces.Commands;

public record CreatePropertyTraceCommand : IRequest<PropertyTrace>
{
	public required DateTime DateSale { get; init; }
	public required string Name { get; init; }
	public required decimal Value { get; init; }
	public required decimal Tax { get; init; }
	public string PropertyId { get; init; } = string.Empty;

	public class CreatePropertyTraceCommandHandler : IRequestHandler<CreatePropertyTraceCommand, PropertyTrace>
	{
		private readonly IPropertyTraceRepository _traceRepository;
		private readonly IPropertyRepository _propertyRepository;

		public CreatePropertyTraceCommandHandler(
				IPropertyTraceRepository traceRepository,
				IPropertyRepository propertyRepository)
		{
			_traceRepository = traceRepository;
			_propertyRepository = propertyRepository;
		}

		public async Task<PropertyTrace> Handle(CreatePropertyTraceCommand request, CancellationToken cancellationToken)
		{
			if (!ObjectId.TryParse(request.PropertyId, out var propertyId))
			{
				throw new ApiException("Invalid property ID format", StatusCodes.Status400BadRequest);
			}

			await _propertyRepository.GetByIdAsync(propertyId);

			var propertyTrace = new PropertyTrace
			{
				PropertyId = propertyId,
				DateSale = request.DateSale,
				Name = request.Name,
				Value = request.Value,
				Tax = request.Tax
			};

			return await _traceRepository.CreateAsync(propertyTrace);
		}
	}
}