using FluentValidation;

namespace MillionLuxury.Application.Features.Owners.Commands;

public class CreateOwnerCommandValidator : AbstractValidator<CreateOwnerCommand>
{
	public CreateOwnerCommandValidator()
	{
		RuleFor(x => x.Name)
				.NotEmpty().WithMessage("Owner name is required")
				.MaximumLength(200).WithMessage("Name cannot exceed 200 characters");

		RuleFor(x => x.Address)
				.NotEmpty().WithMessage("Address is required")
				.MaximumLength(500).WithMessage("Address cannot exceed 500 characters");

		RuleFor(x => x.Photo)
				.NotEmpty().WithMessage("Photo URL is required")
				.Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
				.WithMessage("Photo URL must be a valid URL");

		RuleFor(x => x.Birthday)
				.NotEmpty().WithMessage("Birthday is required")
				.Must(birthday => birthday < DateTime.UtcNow)
				.WithMessage("Birthday cannot be in the future");
	}
}