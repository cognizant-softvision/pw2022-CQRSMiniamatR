using MediatR;

namespace minimalTR.Ping;

//Implements IRequest and return a string
public class PingRequest : IRequest<string>
{
    public string? Message { get; set; }
}
