using FluentValidation;
using minimalTR_core.Attendee;

namespace minimalTR_handlers.Attendee;
public class CreateAttendeeCommandValidator : AbstractValidator<CreateAttendeeCommand>
{
    public CreateAttendeeCommandValidator()
    {
        RuleFor(x => x.Age).GreaterThan(17).WithMessage("Attendee should be have at least 18 years old");

        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
    }
}
