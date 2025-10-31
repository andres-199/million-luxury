namespace MillionLuxury.API.Models;

public record CreatePropertyRequest
{
	public required string Name { get; init; }
	public required string Address { get; init; }
	public decimal Price { get; init; }
	public required string CodeInternal { get; init; }
	public int Year { get; init; }
	public required string OwnerId { get; init; }
}