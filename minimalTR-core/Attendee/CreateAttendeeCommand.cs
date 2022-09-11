using MediatR;

namespace minimalTR_core.Attendee;

public class CreateAttendeeCommand: IRequest<OperationResult>
{
    public string Name { get; set; }
    public int Age { get; set; }
}
