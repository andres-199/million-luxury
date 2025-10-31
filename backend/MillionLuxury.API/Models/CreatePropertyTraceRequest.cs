namespace MillionLuxury.API.Models;

public record CreatePropertyTraceRequest
{
	public required DateTime DateSale { get; init; }
	public required string Name { get; init; }
	public required decimal Value { get; init; }
	public required decimal Tax { get; init; }
}