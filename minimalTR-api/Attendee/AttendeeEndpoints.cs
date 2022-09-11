using MediatR;
using minimalTR_core.Attendee;

namespace minimalTR_full.Attendee;

public static class AttendeeEndpoints
{
    const string ApiGroup = "Attendees";

    public static IEndpointRouteBuilder MapAtteendeeEnpoints(
            this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/attendee", CreateAtteendee).WithTags(ApiGroup);

        return endpoints;
    }

    public static async Task<IResult> CreateAtteendee(AttendeeDTO Attendee, IMediator mediator)
    {
        var result = await mediator.Send(new CreateAttendeeCommand { Name = Attendee.Name, Age = Attendee.Age });

        if (result.Sucess)
        {
            return Results.Created($"/attendee{result.Value}", new { Id = result.Value });
        }

        return Results.Problem();
    }
}
