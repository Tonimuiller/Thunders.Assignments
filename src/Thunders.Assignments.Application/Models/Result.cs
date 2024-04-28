using Thunders.Assignments.Application.Enums;

namespace Thunders.Assignments.Application.Models;

public class Result
{
    protected Result(
        bool isSuccess,
        ResultType type,
        string? message = null)
    {
        IsSuccess = isSuccess;
        Type = type;
        Message = message;
    }

    public bool IsSuccess { get; }
    public ResultType Type { get; }
    public string? Message { get; }

    public static Result Ok(string? message = null) =>
        new Result(true, ResultType.Ok, message);

    public static Result Created(string? message = null) =>
        new Result(true, ResultType.Created, message);

    public static Result BadRequest(string message) =>
        new Result(false, ResultType.BadRequest, message);

    public static Result NotFound(string message) =>
        new Result(false, ResultType.NotFound, message);

    public static Result Conflict(string message) =>
        new Result(false, ResultType.Conflict, message);

    public static Result Error(string message) =>
        new Result(false, ResultType.Error, message);
}
