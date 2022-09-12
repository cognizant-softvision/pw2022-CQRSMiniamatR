using FluentValidation;
using minimalTR_core.Attendee;

namespace minimalTR_handlers.Attendee;
public class CreateAttendeeCommandValidator : AbstractValidator<CreateAttendeeCommand>
{
    public CreateAttendeeCommandValidator()
    {
        RuleFor(x => x.Age).GreaterThan(18);

        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
    }
}
