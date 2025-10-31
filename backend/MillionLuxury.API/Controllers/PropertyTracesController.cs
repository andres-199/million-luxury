using MediatR;
using Microsoft.AspNetCore.Mvc;
using MillionLuxury.Application.Features.PropertyTraces.Commands;
using MillionLuxury.Domain.Entities;
using MillionLuxury.API.Models;

namespace MillionLuxury.API.Controllers;

[ApiController]
[Route("api/properties/{propertyId}/traces")]
public class PropertyTracesController : ControllerBase
{
	private readonly IMediator _mediator;

	public PropertyTracesController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	[ProducesResponseType(typeof(PropertyTrace), StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Create(string propertyId, [FromBody] CreatePropertyTraceRequest request)
	{
		var command = new CreatePropertyTraceCommand
		{
			PropertyId = propertyId,
			DateSale = request.DateSale,
			Name = request.Name,
			Value = request.Value,
			Tax = request.Tax
		};

		var trace = await _mediator.Send(command);
		return Created($"/api/properties/{propertyId}/traces/{trace.Id}", trace);
	}
}