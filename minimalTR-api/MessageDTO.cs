using System.Reflection;

public class MessageDto
{
    public string Message { get; set; }
    public static ValueTask<MessageDto?> BindAsync(HttpContext httpContext, ParameterInfo parameter)
    {
        // int.TryParse(httpContext.Request.Query["message"], out var message);

        var message = httpContext.Request.Query["message"];
        return ValueTask.FromResult<MessageDto?>(
            new MessageDto { Message = message }
        );
    }
}