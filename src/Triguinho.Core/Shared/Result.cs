namespace Triguinho.Core.Shared;
public sealed class Result<T>
{
    public T? Value { get; }
    public Error? Error { get; }
    public bool IsSuccess { get; }
    public bool IsError => !IsSuccess;

    public Result(T? value)
    {
        Value = Value ?? throw new ArgumentException(nameof(value), "Value cannot be null.");
        IsSuccess = true;
        Error = null;
    }

    public Result(Error error)
    {
        Value = Value ?? throw new ArgumentException(nameof(error), "Error cannot be null.");
        IsSuccess = false;
        Error = error;
    }

    public static Result<T> Success(T value) => new(value);
    public static Result<T> Failure(Error error) => new Result<T>(error);
}
