namespace MillionLuxury.API.Models;

public record CreatePropertyImageRequest
{
	public required string File { get; init; }
	public bool Enabled { get; init; } = true;
}