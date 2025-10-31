using MediatR;
using Microsoft.AspNetCore.Mvc;
using MillionLuxury.Application.Features.PropertyImages.Commands;
using MillionLuxury.Domain.Entities;
using MillionLuxury.API.Models;

namespace MillionLuxury.API.Controllers;

[ApiController]
[Route("api/properties/{propertyId}/images")]
public class PropertyImagesController : ControllerBase
{
	private readonly IMediator _mediator;

	public PropertyImagesController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	[ProducesResponseType(typeof(PropertyImage), StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Create(string propertyId, [FromBody] CreatePropertyImageRequest request)
	{
		var command = new CreatePropertyImageCommand
		{
			PropertyId = propertyId,
			File = request.File,
			Enabled = request.Enabled
		};

		var image = await _mediator.Send(command);
		return Created($"/api/properties/{propertyId}/images/{image.Id}", image);
	}
}