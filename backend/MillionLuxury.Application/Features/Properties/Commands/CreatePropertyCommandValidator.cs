using FluentValidation;
using MongoDB.Bson;

namespace MillionLuxury.Application.Features.Properties.Commands;

public class CreatePropertyCommandValidator : AbstractValidator<CreatePropertyCommand>
{
	public CreatePropertyCommandValidator()
	{
		RuleFor(x => x.Name)
				.NotEmpty().WithMessage("Property name is required")
				.MaximumLength(200).WithMessage("Name cannot exceed 200 characters");

		RuleFor(x => x.Address)
				.NotEmpty().WithMessage("Address is required")
				.MaximumLength(500).WithMessage("Address cannot exceed 500 characters");

		RuleFor(x => x.Price)
				.GreaterThan(0).WithMessage("Price must be greater than 0");

		RuleFor(x => x.CodeInternal)
				.NotEmpty().WithMessage("Internal code is required")
				.MaximumLength(50).WithMessage("Internal code cannot exceed 50 characters");

		RuleFor(x => x.Year)
				.GreaterThan(1800).WithMessage("Year must be greater than 1800")
				.LessThanOrEqualTo(DateTime.Now.Year).WithMessage("Year cannot be in the future");

		RuleFor(x => x.OwnerId)
				.NotEmpty().WithMessage("Owner ID is required")
				.Must(id => ObjectId.TryParse(id, out _))
				.WithMessage("Owner ID must be a valid ObjectId");
	}
}