using MediatR;

namespace minimalTR_core.Ping;

//Implements IRequest and return a string
public class PingRequest : IRequest<string>
{
    public string? Message { get; set; }
}
