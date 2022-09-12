using FluentValidation;
using MediatR;
using minimalTR_core;

namespace minimalTR_api;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
    where TResponse : OperationResult
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        if (_validators == null || !_validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        var errors = _validators
            .Select(x => x.Validate(context))
            .SelectMany(x => x.Errors)
            .Where(x => x is not null)
            .GroupBy(
                x => x.PropertyName,
                x => x.ErrorMessage,
                (propertyName, errorMessages) => new
                {
                    Key = propertyName,
                    Values = errorMessages.Distinct().ToArray()
                })
            .ToDictionary(x => x.Key, x => x.Values);

        if (errors.Any())
        {
            var responseType = typeof(TResponse);

            if (responseType.IsGenericType)
            {
                var resultType = responseType.GetGenericArguments()[0];
                var response = typeof(OperationResult<>).MakeGenericType(resultType);
                return Activator.CreateInstance(response, errors) as TResponse;
            }

            return (TResponse)OperationResult.Error(errors);
        }

        return await next();
    }
}
