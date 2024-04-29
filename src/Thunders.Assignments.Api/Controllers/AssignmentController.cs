using MediatR;
using Microsoft.AspNetCore.Mvc;
using Thunders.Assignments.Application.Enums;
using Thunders.Assignments.Application.Features.Assignment.Add;
using Thunders.Assignments.Application.Features.Assignment.Delete;
using Thunders.Assignments.Application.Features.Assignment.Get;
using Thunders.Assignments.Application.Features.Assignment.List;
using Thunders.Assignments.Application.Features.Assignment.Update;
using Thunders.Assignments.Application.Models;

namespace Thunders.Assignments.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class AssignmentController : ControllerBase
{
    private ISender _sender;

    public AssignmentController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetAssignmentRequest(id), cancellationToken);
        return ToActionResult(result);
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] ListAssignmentRequest request, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(request, cancellationToken);
        return ToActionResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AddAssignmentRequest request, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(request, cancellationToken);
        return ToActionResult(result);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateAssignmentRequest request, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(request, cancellationToken);
        return ToActionResult(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteAssignmentRequest request, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(request, cancellationToken);
        return ToActionResult(result);
    }

    private IActionResult ToActionResult<TValue>(Result<TValue> result, string redirectUri = "")
        where TValue: class
    {
        if (result.Type == ResultType.Created)
        {
            return Created(redirectUri, result.Value);
        }

        if (result.Type == ResultType.Ok)
        {
            return Ok(result.Value);
        }

        if (result.Type == ResultType.NotFound)
        {
            return NotFound(result.Message);
        }

        if (result.Type == ResultType.Conflict)
        {
            return Conflict(result.Message);
        }

        if (result.Type == ResultType.BadRequest)
        {
            return BadRequest(result.Message);
        }

        return StatusCode(500);
    }

    private IActionResult ToActionResult(Result result, string redirectUri = "")
    {
        if (result.Type == ResultType.Created)
        {
            return Created(redirectUri, result.Message);
        }

        if (result.Type == ResultType.Ok)
        {
            return Ok(result.Message);
        }

        if (result.Type == ResultType.NotFound)
        {
            return NotFound(result.Message);
        }

        if (result.Type == ResultType.Conflict)
        {
            return Conflict(result.Message);
        }

        if (result.Type == ResultType.BadRequest)
        {
            return BadRequest(result.Message);
        }

        return StatusCode(500);
    }
}
