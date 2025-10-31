using MediatR;
using MillionLuxury.Application.Interfaces;
using MillionLuxury.Domain.Entities;

namespace MillionLuxury.Application.Features.Properties.Queries;

public record GetPropertiesQuery : IRequest<IEnumerable<Property>>;

public class GetPropertiesQueryHandler : IRequestHandler<GetPropertiesQuery, IEnumerable<Property>>
{
	private readonly IPropertyRepository _propertyRepository;

	public GetPropertiesQueryHandler(IPropertyRepository propertyRepository)
	{
		_propertyRepository = propertyRepository;
	}

	public async Task<IEnumerable<Property>> Handle(GetPropertiesQuery request, CancellationToken cancellationToken)
	{
		return await _propertyRepository.GetAllAsync();
	}
}