using FluentValidation;
using MongoDB.Bson;

namespace MillionLuxury.Application.Features.PropertyImages.Commands;

public class CreatePropertyImageCommandValidator : AbstractValidator<CreatePropertyImageCommand>
{
	public CreatePropertyImageCommandValidator()
	{
		RuleFor(x => x.PropertyId)
				.NotEmpty().WithMessage("Property ID is required")
				.Must(id => ObjectId.TryParse(id, out _))
				.WithMessage("Property ID must be a valid ObjectId");

		RuleFor(x => x.File)
				.NotEmpty().WithMessage("Image file URL is required")
				.Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
				.WithMessage("Image file URL must be a valid URL");
	}
}