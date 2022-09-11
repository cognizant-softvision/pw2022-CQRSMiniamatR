using MediatR;

namespace minimalTR.Ping;

public class PingHandler : IRequestHandler<PingRequest, string>
{
    public async Task<string> Handle(PingRequest request, CancellationToken cancellationToken)
    {
        return await Task.FromResult( $"Pong! Your message was {request.Message}");
    }
}
