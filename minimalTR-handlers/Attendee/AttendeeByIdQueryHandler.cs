using MediatR;
using Microsoft.EntityFrameworkCore;
using minimalTR_core.Attendee;
using minimalTR_dal;

namespace minimalTR_handlers.Attendee
{
    public class AttendeeByIdQueryHandler : IRequestHandler<AttendeeByIdQuery, Person?>
    {
        public MinimaltrDB MinimaltrDB { get; }

        public AttendeeByIdQueryHandler(MinimaltrDB minimaltrDB)
        {
            MinimaltrDB = minimaltrDB;
        }

        public async Task<Person?> Handle(AttendeeByIdQuery request, CancellationToken cancellationToken)
        {
            var attendee = await MinimaltrDB.Attendees.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (attendee is null)
            {
                return null;
            }

            return new Person
            {
                Id = attendee.Id,
                Age = attendee.Age,
                Name = attendee.Name
            };
        }
    }
}
