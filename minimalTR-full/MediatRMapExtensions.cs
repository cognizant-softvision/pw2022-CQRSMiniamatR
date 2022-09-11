using MediatR;

public static class MediatRMapExtensions
{
    public static RouteHandlerBuilder MediatorMapGet<T1, TRequest>(this IEndpointRouteBuilder app, 
        string pattern, 
        Func<T1, TRequest> transformationDelegate)
    {
        return app.MapGet(pattern, async (T1 param1, IMediator mediator) =>
        {
            var request = transformationDelegate(param1);
            var result = await mediator.Send(request);
            return result;
        });
    }

}
