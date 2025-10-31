namespace MillionLuxury.API.Models;

public record CreateOwnerRequest
{
	public required string Name { get; init; }
	public required string Address { get; init; }
	public required string Photo { get; init; }
	public required DateTime Birthday { get; init; }
}