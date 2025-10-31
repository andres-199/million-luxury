using MediatR;
using MongoDB.Bson;
using MillionLuxury.Application.Interfaces;
using MillionLuxury.Domain.Entities;

namespace MillionLuxury.Application.Features.Owners.Commands;

public record CreateOwnerCommand : IRequest<Owner>
{
	public required string Name { get; init; }
	public required string Address { get; init; }
	public required string Photo { get; init; }
	public required DateTime Birthday { get; init; }
}

public class CreateOwnerCommandHandler : IRequestHandler<CreateOwnerCommand, Owner>
{
	private readonly IOwnerRepository _ownerRepository;

	public CreateOwnerCommandHandler(IOwnerRepository ownerRepository)
	{
		_ownerRepository = ownerRepository;
	}

	public async Task<Owner> Handle(CreateOwnerCommand request, CancellationToken cancellationToken)
	{
		var owner = new Owner
		{
			Name = request.Name,
			Address = request.Address,
			Photo = request.Photo,
			Birthday = request.Birthday,
			Properties = new List<Property>()
		};

		return await _ownerRepository.CreateAsync(owner);
	}
}