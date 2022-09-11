using MediatR;

public static class MediatRMapExtensions
{
    public static IEndpointRouteBuilder MediatorMapGet<T1, TRequest>(this IEndpointRouteBuilder app, 
        string pattern, 
        Func<T1, TRequest> transformationDelegate)
    {
        app.MapGet(pattern, async (T1 param1, IMediator mediator) =>
        {
            var request = transformationDelegate(param1);
            var result = await mediator.Send(request);
            return result;
        });
        return app;
    }

}
