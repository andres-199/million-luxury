using MediatR;
using Microsoft.AspNetCore.Mvc;
using MillionLuxury.Application.Features.Owners.Commands;
using MillionLuxury.Domain.Entities;
using MillionLuxury.API.Models;

namespace MillionLuxury.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OwnersController : ControllerBase
{
	private readonly IMediator _mediator;

	public OwnersController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	[ProducesResponseType(typeof(Owner), StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> Create([FromBody] CreateOwnerRequest request)
	{
		var command = new CreateOwnerCommand
		{
			Name = request.Name,
			Address = request.Address,
			Photo = request.Photo,
			Birthday = request.Birthday
		};

		var owner = await _mediator.Send(command);
		return Created($"/api/owners/{owner.Id}", owner);
	}
}