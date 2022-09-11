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
        endpoints.MapGet("/attendee/{id}", GetAttendeeById).WithTags(ApiGroup);

        return endpoints;
    }

    private static async Task<IResult> GetAttendeeById(int id, IMediator mediator)
    {
        var result = await mediator.Send(new AttendeeByIdQuery { Id = id });

        if (result == null)
        {
            return Results.NoContent();
        }

        return Results.Ok(new { result.Id, result.Name, result.Age, InviteText = $"welcome {result.Name}!" });
    }

    public static async Task<IResult> CreateAtteendee(CreateAttendeeDTO Attendee, IMediator mediator)
    {
        var result = await mediator.Send(new CreateAttendeeCommand { Name = Attendee.Name, Age = Attendee.Age });

        if (result.Sucess)
        {
            return Results.Created($"/attendee{result.Value}", new { Id = result.Value });
        }

        return Results.Problem();
    }
}
