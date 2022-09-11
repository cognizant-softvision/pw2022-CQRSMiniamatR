namespace minimalTR_core;

public class OperationResult<T>
{
    public bool Sucess { get; set; }
    public T Value { get; private set; }

    public static OperationResult<T> Success(T value)
    {
        return new OperationResult<T> { Sucess = true, Value = value };
    }
}