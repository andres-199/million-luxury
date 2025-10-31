using MediatR;
using Microsoft.AspNetCore.Mvc;
using MillionLuxury.Application.Features.Properties.Commands;
using MillionLuxury.Application.Features.Properties.Queries;
using MillionLuxury.API.Models;

namespace MillionLuxury.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertiesController : ControllerBase
{
	private readonly IMediator _mediator;

	public PropertiesController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpGet]
	public async Task<IActionResult> GetAll(
		[FromQuery] string? name = null,
		[FromQuery] string? address = null,
		[FromQuery] decimal? minPrice = null,
		[FromQuery] decimal? maxPrice = null,
		[FromQuery] int page = 1,
		[FromQuery] int pageSize = 10)
	{
		var query = new GetPropertiesQuery
		{
			Name = name,
			Address = address,
			MinPrice = minPrice,
			MaxPrice = maxPrice,
			Page = page,
			PageSize = pageSize
		};

		var properties = await _mediator.Send(query);
		return Ok(properties);
	}

	[HttpPost]
	public async Task<IActionResult> Create([FromBody] CreatePropertyRequest request)
	{
		var command = new CreatePropertyCommand
		{
			Name = request.Name,
			Address = request.Address,
			Price = request.Price,
			CodeInternal = request.CodeInternal,
			Year = request.Year,
			OwnerId = request.OwnerId
		};

		var property = await _mediator.Send(command);
		return Created($"/api/properties/{property.Id}", property);
	}
}