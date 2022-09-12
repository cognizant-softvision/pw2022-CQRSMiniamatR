namespace minimalTR_core;

public class OperationResult
{
    public bool Success { get; set; }
    public IDictionary<string, string[]>? Errors { get; protected set; }

    public static OperationResult Error(IDictionary<string, string[]> errors)
    {
        return new OperationResult { Success = false, Errors = errors };
    }
}

public class OperationResult<T> : OperationResult
{
    public T? Value { get; private set; }

    public OperationResult(T? value)
    {
        Value = value;
        Success = true;
    }

    public OperationResult(IDictionary<string, string[]> errors)
    {
        Errors = errors;
        Success = false;
    }
}