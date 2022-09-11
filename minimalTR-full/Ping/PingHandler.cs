using MediatR;

namespace minimalTR.Ping;

//implement IRequestHandler<PingRequest, string>
public class PingHandler : IRequestHandler<PingRequest, string>
{
    public async Task<string> Handle(PingRequest request, CancellationToken cancellationToken)
    {
        return await Task.FromResult( $"Pong! Your message was {request.Message}");
    }
}
