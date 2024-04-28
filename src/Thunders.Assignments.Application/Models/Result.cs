using Thunders.Assignments.Application.Enums;

namespace Thunders.Assignments.Application.Models;

public class Result
{
    protected Result(
        ResultType type,
        string? message = null)
    {
        Type = type;
        Message = message;
    }

    public ResultType Type { get; }
    public string? Message { get; }

    public static Result Ok(string? message = null) =>
        new Result(ResultType.Ok, message);

    public static Result Created(string? message = null) =>
        new Result(ResultType.Created, message);

    public static Result BadRequest(string message) =>
        new Result(ResultType.BadRequest, message);

    public static Result NotFound(string message) =>
        new Result(ResultType.NotFound, message);

    public static Result Conflict(string message) =>
        new Result(ResultType.Conflict, message);

    public static Result Error(string message) =>
        new Result(ResultType.Error, message);
}
