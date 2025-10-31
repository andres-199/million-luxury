using MediatR;
using MillionLuxury.Application.Interfaces;
using MillionLuxury.Domain.Entities;

namespace MillionLuxury.Application.Features.Properties.Queries;

public record GetPropertiesQuery : IRequest<IEnumerable<Property>>
{
	public string? Name { get; init; }
	public string? Address { get; init; }
	public decimal? MinPrice { get; init; }
	public decimal? MaxPrice { get; init; }
	public int Page { get; init; } = 1;
	public int PageSize { get; init; } = 10;
}

public class GetPropertiesQueryHandler : IRequestHandler<GetPropertiesQuery, IEnumerable<Property>>
{
	private readonly IPropertyRepository _propertyRepository;

	public GetPropertiesQueryHandler(IPropertyRepository propertyRepository)
	{
		_propertyRepository = propertyRepository;
	}

	public async Task<IEnumerable<Property>> Handle(GetPropertiesQuery request, CancellationToken cancellationToken)
	{
		return await _propertyRepository.GetByFilterAsync(
			request.Name,
			request.Address,
			request.MinPrice,
			request.MaxPrice,
			request.Page,
			request.PageSize);
	}
}