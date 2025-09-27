namespace Triguinho.Core.Shared;
public sealed class Result<T>
{
    public T? Value { get; }
    public Error? Error { get; }
    public bool IsSuccess { get; }
    public bool IsError => !IsSuccess;

    private Result(T? value)
    {
        Value = value ?? throw new ArgumentException(nameof(value), "Value cannot be null.");
        IsSuccess = true;
        Error = null;
    }

    private Result(Error error)
    {
        Error = error ?? throw new ArgumentException(nameof(error), "Error cannot be null.");
        IsSuccess = false;
        Error = error;
    }

    public static Result<T> Success(T value) => new(value);
    public static Result<T> Failure(Error error) => new Result<T>(error);
}
