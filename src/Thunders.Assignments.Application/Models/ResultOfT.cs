using Thunders.Assignments.Application.Enums;

namespace Thunders.Assignments.Application.Models;

public class Result<TValue> : Result
    where TValue: class
{
    private Result(
        bool isSuccess,
        TValue value,
        ResultType type,
        string? message = null) : base(isSuccess, type, message)
    {
        Value = value;
    }

    public TValue? Value { get; }

    public static Result<TValue> Ok(TValue value, string? message = null) =>
        new Result<TValue>(true, value, ResultType.Ok, message);

    public static Result<TValue> Created(TValue value, string? message = null) =>
        new Result<TValue>(true, value, ResultType.Created, message);

    new public static Result<TValue> BadRequest(string message) =>
        new Result<TValue>(false, null!, ResultType.BadRequest, message);

    new public static Result NotFound(string message) =>
        new Result<TValue>(false, null!, ResultType.NotFound, message);

    new public static Result Conflict(string message) =>
        new Result<TValue>(false, null!, ResultType.Conflict, message);

    new public static Result Error(string message) =>
        new Result<TValue>(false, null!, ResultType.Error, message);
}
