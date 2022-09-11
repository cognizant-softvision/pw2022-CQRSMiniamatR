using MediatR;

namespace minimalTR_core.Attendee
{
    public class AttendeeByIdQuery : IRequest<Person?>
    {
        public int Id { get; set; }
    }
}
