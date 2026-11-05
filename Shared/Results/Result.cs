namespace sobrou.Shared.Results;

public sealed class Result<T>
{
    public T? Value { get; private set; }
    public Failure? Error { get; private set; }
    public bool IsSuccess => Error == null;
    public bool IsFailure => !IsSuccess;

    private Result(T value)  { Value = value; }
    private Result(Failure error) { Error = error; }
    public static Result<T> Success (T value) => new(value);
    public static Result<T> Failure (Failure error) => new(error);
}