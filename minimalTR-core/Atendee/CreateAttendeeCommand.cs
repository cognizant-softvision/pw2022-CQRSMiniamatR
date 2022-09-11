using MediatR;

namespace minimalTR_core.Atendee;

public class CreateAttendeeCommand: IRequest<OperationResult>
{
    public string Name { get; set; }
    public int Age { get; set; }
}
