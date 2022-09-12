using MediatR;
using minimalTR_core;
using minimalTR_core.Attendee;
using minimalTR_dal;
using minimalTR_dal.Attendee;

namespace minimalTR_handlers.Attendee;

public class CreateAttendeeCommandHandler : IRequestHandler<CreateAttendeeCommand, OperationResult<int>>
{
    public MinimaltrDB MinimaltrDB { get; }

    public CreateAttendeeCommandHandler(MinimaltrDB minimaltrDB)
    {
        MinimaltrDB = minimaltrDB;
    }

    public async Task<OperationResult<int>> Handle(CreateAttendeeCommand request, CancellationToken cancellationToken)
    {
        var attendeeInfo = new AttendeeInformation
        {
            Age = request.Age,
            Name = request.Name
        };

        MinimaltrDB.Attendees.Add(attendeeInfo);

        await MinimaltrDB.SaveChangesAsync();

        return new OperationResult<int>(attendeeInfo.Id);
    }
}
