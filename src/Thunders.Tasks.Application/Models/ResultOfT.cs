using Thunders.Assignments.Application.Enums;

namespace Thunders.Assignments.Application.Models;

public class Result<TValue> : Result
{
    private Result(
        TValue value,
        ResultType type,
        string? message = null) : base(type, message)
    {
        Value = value;
    }

    public TValue? Value { get; }

    public static Result<TValue> Ok(TValue value, string? message = null) =>
        new Result<TValue>(value, ResultType.Ok, message);

    public static Result<TValue> Created(TValue value, string? message = null) =>
        new Result<TValue>(value, ResultType.Created, message);
}
