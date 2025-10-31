using FluentValidation;
using MongoDB.Bson;

namespace MillionLuxury.Application.Features.PropertyTraces.Commands;

public class CreatePropertyTraceCommandValidator : AbstractValidator<CreatePropertyTraceCommand>
{
	public CreatePropertyTraceCommandValidator()
	{
		RuleFor(x => x.PropertyId)
				.NotEmpty().WithMessage("Property ID is required")
				.Must(id => ObjectId.TryParse(id, out _))
				.WithMessage("Property ID must be a valid ObjectId");

		RuleFor(x => x.DateSale)
				.NotEmpty().WithMessage("Sale date is required")
				.Must(date => date <= DateTime.UtcNow)
				.WithMessage("Sale date cannot be in the future");

		RuleFor(x => x.Name)
				.NotEmpty().WithMessage("Name is required")
				.MaximumLength(200).WithMessage("Name cannot exceed 200 characters");

		RuleFor(x => x.Value)
				.GreaterThan(0).WithMessage("Value must be greater than 0");

		RuleFor(x => x.Tax)
				.GreaterThanOrEqualTo(0).WithMessage("Tax cannot be negative");
	}
}